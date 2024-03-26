using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Bookings;
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
            result.Should().HaveCount(5);
        }

        [Test]
        public async Task AddBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction(); 

            OuthouseBookingService service = new(dbContext, MemberContext);
            AddBookingRequest request = new("guest@outhouse.com", DateOnly.Parse("2000-01-01"), DateOnly.Parse("2000-01-02"));

            BookingDto result = await service.AddBookingAsync(OuthouseId, request);
            
            dbContext.ChangeTracker.Clear(); 

            Booking? booking = dbContext.Bookings.Where(x => x.Id == result.Id).SingleOrDefault();
            
            using (new AssertionScope())
            {
                result.State.Should().Be("Requested");
                booking.Should().NotBeNull();
                booking?.Id.Should().Be(result.Id);
                booking?.State.Should().Be(BookingState.Requested);
            }
        }

        [Test]
        public async Task ApproveBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();

            Guid bookingId = new("5990aea7-1c7b-48b1-8d18-de00bf98a7b6");
            OuthouseBookingService service = new(dbContext, AdminContext);

            await service.ApproveBookingAsync(OuthouseId, bookingId);

            dbContext.ChangeTracker.Clear();

            Booking booking = dbContext.Bookings.Where(x => x.Id == bookingId).Single();
            booking.State.Should().Be(BookingState.Approved);
        }

        [Test]
        public async Task RejectBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();

            Guid bookingId = new("5990aea7-1c7b-48b1-8d18-de00bf98a7b7");
            OuthouseBookingService service = new(dbContext, AdminContext);

            await service.RejectBookingAsync(OuthouseId, bookingId);

            dbContext.ChangeTracker.Clear();

            Booking booking = dbContext.Bookings.Where(x => x.Id == bookingId).Single();
            booking.State.Should().Be(BookingState.Rejected);
        }

        [Test]
        public async Task DeleteBooking()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();

            Guid bookingId = new("5990aea7-1c7b-48b1-8d18-de00bf98a7b5");
            OuthouseBookingService service = new(dbContext, MemberContext);

            await service.DeleteBookingAsync(OuthouseId, bookingId);

            dbContext.ChangeTracker.Clear();

            Booking? booking = dbContext.Bookings.Where(x => x.Id == bookingId).SingleOrDefault();
            booking.Should().BeNull();  
        }
    }
}
