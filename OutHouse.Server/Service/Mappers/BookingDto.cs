using OutHouse.Server.Domain.Bookings;

namespace OutHouse.Server.Service.Mappers
{
    public record class BookingDto(Guid Id, string BookerEmail, DateOnly Start, DateOnly End, string State) : IEntity;
}
