using OutHouse.Server.Models;

namespace OutHouse.Server.Domain.Bookings
{
    public class Booking
    {

        public Guid Id { get; set; }

        public Guid OuthouseId { get; set; }

        public Outhouse Outhouse { get; set; } = null!;

        public string BookerEmail { get; set; } = string.Empty;

        public DateOnly Start { get; set; }

        public DateOnly End { get; set; }

        public BookingState State { get; set; }

    }

    public enum BookingState { Requested, Approved, Rejected }
}
