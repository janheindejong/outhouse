using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Bookings;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;
using OutHouse.Server.Service.Mappers;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseBookingService(
            IDbContext dbContext,
            IUserContext userContext,
            Guid outhouseId)
                : ServiceBase(dbContext, userContext)
    {

        private Guid OuthouseId { get; } = outhouseId;

        public async Task<List<BookingDto>> GetBookingsAsync()
        {
            Outhouse outhouse = await GetOuthouseAsync();

            if (!outhouse.HasMember(UserContext.Email))
            {
                throw new ForbiddenException("get bookings of", "Outhouse", OuthouseId);
            }

            return outhouse.Bookings.Select(x => x.ToDto()).ToList();
        }

        public async Task<BookingDto> AddBooking(AddBookingRequest request)
        {
            Outhouse outhouse = await GetOuthouseAsync();
            if (!outhouse.HasMember(UserContext.Email))
            {
                throw new ForbiddenException("request bookings of", "Outhouse", OuthouseId);
            }

            Booking booking = outhouse.AddBookingRequest(request.BookerEmail, request.Start, request.End);
            return booking.ToDto();
        }

        public async Task ApproveBooking(Guid bookingId)
        {
            Outhouse outhouse = await GetOuthouseAsync();
            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("approve", "Booking", bookingId);
            }

            outhouse.ApproveBooking(bookingId);
        }

        public async Task RejectBooking(Guid bookingId)
        {
            Outhouse outhouse = await GetOuthouseAsync();
            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("approve", "Booking", bookingId);
            }

            outhouse.RejectBooking(bookingId);
        }

        public async Task CancelBooking(Guid bookingId)
        {
            Outhouse outhouse = await GetOuthouseAsync();
            Booking booking = outhouse.GetBookingById(bookingId);
            if (booking.BookerEmail != UserContext.Email)
            {
                throw new ForbiddenException("cancel", "Booking", bookingId);
            }

            outhouse.CancelBooking(bookingId);
        }

        private async Task<Outhouse> GetOuthouseAsync()
        {
            return await DbContext.Outhouses
                .Where(x => x.Id == OuthouseId)
                .Include(x => x.Members)
                .Include(x => x.Bookings)
                .SingleOrDefaultAsync()
                    ?? throw new NotFoundException("Outhouse", OuthouseId);
        }
    }
    
    public record struct AddBookingRequest(string BookerEmail, DateOnly Start, DateOnly End);
}
