using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Bookings;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;
using OutHouse.Server.Service.Mappers;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseBookingService(
            IDbContext dbContext,
            IUserContext userContext)
                : ServiceBase(dbContext, userContext)
    {

        public async Task<List<BookingDto>> GetBookingsAsync(Guid outhouseId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);

            if (!outhouse.HasMember(UserContext.Email))
            {
                throw new ForbiddenException("get bookings of", "Outhouse", outhouseId);
            }

            return outhouse.Bookings.Select(x => x.ToDto()).ToList();
        }

        public async Task<BookingDto> AddBooking(Guid outhouseId, AddBookingRequest request)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);
            if (!outhouse.HasMember(UserContext.Email))
            {
                throw new ForbiddenException("request bookings of", "Outhouse", outhouseId);
            }

            Booking booking = outhouse.AddBookingRequest(request.BookerEmail, request.Start, request.End);
            return booking.ToDto();
        }

        public async Task ApproveBooking(Guid outhouseId, Guid bookingId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);
            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("approve", "Booking", bookingId);
            }

            outhouse.ApproveBooking(bookingId);
        }

        public async Task RejectBooking(Guid outhouseId, Guid bookingId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);
            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("approve", "Booking", bookingId);
            }

            outhouse.RejectBooking(bookingId);
        }

        public async Task CancelBooking(Guid outhouseId, Guid bookingId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);
            Booking booking = outhouse.GetBookingById(bookingId);
            if (booking.BookerEmail != UserContext.Email)
            {
                throw new ForbiddenException("cancel", "Booking", bookingId);
            }

            outhouse.CancelBooking(bookingId);
        }

        private async Task<Outhouse> GetOuthouseAsync(Guid outhouseId)
        {
            return await DbContext.Outhouses
                .Where(x => x.Id == outhouseId)
                .Include(x => x.Members)
                .Include(x => x.Bookings)
                .SingleOrDefaultAsync()
                    ?? throw new NotFoundException("Outhouse", outhouseId);
        }
    }
    
    public record struct AddBookingRequest(string BookerEmail, DateOnly Start, DateOnly End);
}
