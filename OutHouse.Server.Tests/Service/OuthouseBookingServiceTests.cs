using FluentAssertions;
using FluentAssertions.Execution;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;

namespace OutHouse.Server.Tests.Service
{
    internal class OuthouseBookingServiceTests : ServiceTestBase
    {

        [Test]
        public async Task GetBookings()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            OuthouseBookingService service = new(dbContext, MemberContext);
            List<BookingDto> result = await service.GetBookingsAsync(OuthouseId);
            result.Should().HaveCount(4);
        }

        [Test]
        public async Task AddBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction(); 
            OuthouseBookingService service = new(dbContext, MemberContext);
            AddBookingRequest request = new("guest@outhouse.com", DateOnly.Parse("2000-01-01"), DateOnly.Parse("2000-01-02"));
            BookingDto result = await service.AddBooking(OuthouseId, request);
            using (new AssertionScope())
            {
                result.State.Should().Be("Requested");
            }

            dbContext.ChangeTracker.Clear(); 
        }

        [Test]
        public async Task ApproveBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();
            Guid bookingId = new("5990aea7-1c7b-48b1-8d18-de00bf98a7b5");
            OuthouseBookingService service = new(dbContext, AdminContext);
            await service.ApproveBooking(OuthouseId, bookingId);
            BookingDto booking = (await service.GetBookingsAsync(OuthouseId)).Where(x => x.Id == bookingId).First();
            booking.State.Should().Be("Approved");
            dbContext.ChangeTracker.Clear();
        }

        [Test]
        public async Task RejectBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();
            Guid bookingId = new("5990aea7-1c7b-48b1-8d18-de00bf98a7b5");
            OuthouseBookingService service = new(dbContext, AdminContext);
            await service.RejectBooking(OuthouseId, bookingId);
            BookingDto booking = (await service.GetBookingsAsync(OuthouseId)).Where(x => x.Id == bookingId).First();
            booking.State.Should().Be("Rejected");
            dbContext.ChangeTracker.Clear();
        }

        [Test]
        public async Task CancelBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();
            Guid bookingId = new("5990aea7-1c7b-48b1-8d18-de00bf98a7b5");
            OuthouseBookingService service = new(dbContext, MemberContext);
            await service.CancelBooking(OuthouseId, bookingId);
            BookingDto booking = (await service.GetBookingsAsync(OuthouseId)).Where(x => x.Id == bookingId).First();
            booking.State.Should().Be("Cancelled");
            dbContext.ChangeTracker.Clear();
        }
    }
}
