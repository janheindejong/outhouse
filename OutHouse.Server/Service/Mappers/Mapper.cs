using OutHouse.Server.Domain.Bookings;
using OutHouse.Server.Domain.Members;
using OutHouse.Server.Models;

namespace OutHouse.Server.Service.Mappers
{
    public static class Mapper
    {
        public static OuthouseDto ToDto(this Outhouse outhouse)
        {
            return new(outhouse.Id, outhouse.Name);
        }

        public static MemberDto ToDto(this Member member)
        {
            return new(member.Id, member.Email, member.Name);
        }

        public static BookingDto ToDto(this Booking booking)
        {
            return new(booking.Id, booking.BookerEmail, booking.Start, booking.End, booking.State);
        }
    }
}
