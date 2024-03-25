using FluentAssertions;
using FluentAssertions.Execution;
using OutHouse.Server.Domain.Bookings;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;

namespace OutHouse.Server.Tests.Domain
{
    public class OuthouseBookingsTests
    {

        [Test]
        public void AddBookingRequest()
        {
            Outhouse outhouse = GetPopulatedOuthouse();
            Booking booking = outhouse.AddBookingRequest("member@outhouse.com", "2000-01-01", "2000-01-02");

            using (new AssertionScope())
            {
                booking.OuthouseId.Should().Be(outhouse.Id);
                booking.State.Should().Be(BookingState.Requested);
                outhouse.Bookings.Should().Contain(booking);
            }
        }

        [Test] 
        public void AddBookingRequest_WrongDateOrder_ThrowsBadRequest()
        {
            Outhouse outhouse = GetPopulatedOuthouse();
            Func<Booking> act = () => outhouse.AddBookingRequest("member@outhouse.com", "2000-01-02", "2000-01-01");
            act.Should().Throw<BadRequestException>();
        }

        [Test]
        public void RejectBooking()
        {
            Outhouse outhouse = GetPopulatedOuthouse();
            Booking booking = outhouse.AddBookingRequest("member@outhouse.com", "2000-01-04", "2000-01-05");
            outhouse.RejectBooking(booking.Id);
            booking.State.Should().Be(BookingState.Rejected);
        }

        [Test]
        public void CancelBooking()
        {
            Outhouse outhouse = GetPopulatedOuthouse();
            Booking booking = outhouse.AddBookingRequest("member@outhouse.com", "2000-01-04", "2000-01-05");
            outhouse.CancelBooking(booking.Id);
            booking.State.Should().Be(BookingState.Cancelled);
        }

        [Test]
        public void ApproveBooking()
        {
            Outhouse outhouse = GetPopulatedOuthouse();
            Booking booking = outhouse.AddBookingRequest("member@outhouse.com", "2000-01-04", "2000-01-05");
            outhouse.ApproveBooking(booking.Id);
            booking.State.Should().Be(BookingState.Approved);
        }

        [Test]
        public void ApproveBooking_AlreadyBooked_ThrowsNotAllowed()
        {
            Outhouse outhouse = GetPopulatedOuthouse();
            Booking booking = outhouse.AddBookingRequest("member@outhouse.com", "2000-01-01", "2000-01-03");
            Action act = () => outhouse.ApproveBooking(booking.Id);
            act.Should().Throw<ConflictException>();
        }

        [TestCase("2000-01-04", "2000-01-05", ExpectedResult = true)]
        [TestCase("2000-01-03", "2000-01-04", ExpectedResult = false)]
        [TestCase("1999-12-30", "1999-12-31", ExpectedResult = true)]
        [TestCase("1999-12-30", "2000-01-01", ExpectedResult = false)]
        [TestCase("1999-12-30", "2000-01-05", ExpectedResult = false)]
        public bool IsFreeBetween(string start, string end)
        {
            Outhouse outhouse = GetPopulatedOuthouse();
            return outhouse.IsFreeBetween(DateOnly.Parse(start), DateOnly.Parse(end));
        }

        private static Outhouse GetPopulatedOuthouse()
        {
            Outhouse outhouse = new()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            outhouse.Bookings.Add(new Booking()
            {
                Id = Guid.NewGuid(),
                OuthouseId = outhouse.Id,
                Start = DateOnly.Parse("2000-01-01"),
                End = DateOnly.Parse("2000-01-03"),
                State = BookingState.Approved
            });

            outhouse.Bookings.Add(new Booking()
            {
                Id = Guid.NewGuid(),
                OuthouseId = outhouse.Id,
                Start = DateOnly.Parse("2000-01-04"),
                End = DateOnly.Parse("2000-01-05"),
                State = BookingState.Requested
            });

            outhouse.Bookings.Add(new Booking()
            {
                Id = Guid.NewGuid(),
                OuthouseId = outhouse.Id,
                Start = DateOnly.Parse("2000-01-04"),
                End = DateOnly.Parse("2000-01-05"),
                State = BookingState.Rejected
            });

            outhouse.Bookings.Add(new Booking()
            {
                Id = Guid.NewGuid(),
                OuthouseId = outhouse.Id,
                Start = DateOnly.Parse("2000-01-04"),
                End = DateOnly.Parse("2000-01-05"),
                State = BookingState.Cancelled
            });

            return outhouse;
        }
    }
}
