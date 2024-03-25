using System.Data;
using OutHouse.Server.Domain.Bookings;
using OutHouse.Server.Domain.Exceptions;

namespace OutHouse.Server.Models
{
    public partial class Outhouse
    {

        public ICollection<Booking> Bookings { get; } = [];

        public Booking AddBookingRequest(string bookerEmail, string start, string end)
        {
            return AddBookingRequest(bookerEmail, DateOnly.Parse(start), DateOnly.Parse(end));
        }

        public Booking AddBookingRequest(string bookerEmail, DateOnly start, DateOnly end)
        {
            if (start > end)
            {
                throw new BadRequestException("Booking start date can't be after end date");
            }
            
            Booking booking = new()
            {
                Id = Guid.NewGuid(),
                OuthouseId = Id,
                BookerEmail = bookerEmail,
                Start = start,
                End = end,
                State = BookingState.Requested
            };

            Bookings.Add(booking);
            return booking;
        }

        public void ApproveBooking(Guid bookingId)
        {
            Booking booking = GetBookingById(bookingId);

            if (booking.State != BookingState.Requested)
            {
                throw new ConflictException("approve", "booking", bookingId, "Can only approve bookings with state 'Requested'");
            }

            if (!IsFreeBetween(booking.Start, booking.End))
            {
                throw new ConflictException("approve", "booking", bookingId, "Outhouse is already booked during the requested dates");
            }

            booking.State = BookingState.Approved;
        }

        public void RejectBooking(Guid bookingId)
        {
            Booking booking = GetBookingById(bookingId);
            booking.State = BookingState.Rejected;
        }

        public void CancelBooking(Guid bookingId)
        {
            Booking booking = GetBookingById(bookingId);
            booking.State = BookingState.Cancelled;
        }

        public bool IsFreeBetween(DateOnly start, DateOnly end)
        {
            if (start > end)
            {
                throw new BadRequestException("Booking start date can't be after end date");
            }

            foreach (Booking booking in Bookings.Where(x => x.State == BookingState.Approved))
            {
                if ((start <= booking.End) && (end >= booking.Start))
                {
                    return false;
                }
            }

            return true;
        }

        public Booking GetBookingById(Guid id)
        {
            return Bookings.Where(x => x.Id == id).FirstOrDefault()
                ?? throw new NotFoundException("Booking", id);
        }
    }
}
