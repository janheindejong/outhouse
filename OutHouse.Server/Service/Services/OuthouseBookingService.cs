using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;
using OutHouse.Server.Service.Mappers;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseBookingService(
            IDbContext dbContext,
            IUserContext userContext,
            Guid outhouseId
        )
                : OuthouseServiceBase(dbContext, userContext, outhouseId)
    {

        public async Task<List<BookingDto>> GetBookingsAsync()
        {
            Outhouse outhouse = await GetOuthouseAsync();

            if (!outhouse.HasMember(UserContext.Email))
            {
                throw new ForbiddenException("get bookings of", "Outhouse", OuthouseId);
            }

            return outhouse.Bookings
                .Select(x => x.ToDto())
                .ToList();
        }

    }
}
