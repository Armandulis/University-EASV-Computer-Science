using System;
using System.Collections.Generic;
using System.Diagnostics;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private BookingManager bookingManager;
        Mock<IRepository<Booking>> bookingRepository;
        Mock<IRepository<Room>> roomRepository;

        public BookingManagerTests()
        {
            bookingRepository = new Mock<IRepository<Booking>>();
            roomRepository = new Mock<IRepository<Room>>();
            bookingManager = new BookingManager(bookingRepository.Object, roomRepository.Object);
        }


        #region Tests and Data for FindAvailableRoom

        [Fact]
        public void CreateBooking_NoRooms_ReturnsFalse()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);

            Booking booking = new Booking { Id = 1, StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Today.AddDays(10), CustomerId = 1, RoomId = 1 };

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=DateTime.Today.AddDays(2), EndDate=DateTime.Today.AddDays(6), IsActive=true, CustomerId=2, RoomId=1 },
            };
            bookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);

            // Act
            bool response = bookingManager.CreateBooking(booking);
            // Assert
            Assert.False(response);
        }

        [Fact]
        public void CreateBooking_AviableRoom_ReturnsTrue()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);

            Booking booking = new Booking { Id = 1, StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Today.AddDays(10), CustomerId = 1, RoomId = 1 };

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=DateTime.Today.AddDays(2), EndDate=DateTime.Today.AddDays(3), IsActive=true, CustomerId=2, RoomId=1 },
            };
            bookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);

            roomRepository.Setup(repo => repo.GetAll()).Returns(new List<Room> { new Room() { Id = 1 } });

            // Act
            bool response = bookingManager.CreateBooking(booking);
            // Assert
            Assert.True(response);
        }

        #endregion

        #region Tests and Data for FindAvailableRoom

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            DateTime date = DateTime.Today;
            Assert.Throws<ArgumentException>(() => bookingManager.FindAvailableRoom(date, date));
        }

        [Fact]
        public void FindAvailableRoom_NoAvaibleRooms_RoomIdMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate= DateTime.Today, EndDate=DateTime.Today, IsActive=true, CustomerId=1, RoomId=1 },
            };
            bookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);


            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.Equal(-1, roomId);
        }

        [Fact]
        public void FindAvailableRoom_NoAvialeRooms_RoomIdMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate= DateTime.Today, EndDate=DateTime.Today, IsActive=false, CustomerId=1, RoomId=1 },
            };
            bookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);

            roomRepository.Setup(repo => repo.GetAll()).Returns(new List<Room> { });

            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.Equal(-1, roomId);
        }

        [Theory]
        [MemberData(nameof(GetData_FindAvailableRoom_NoAviableRooms))]
        public void FindAvailableRoom_NoAvialeRoomsAtPeriod_RoomIdMinusOne(DateTime bookedTimeFrom, DateTime bookedTimeTo)
        {
            // Arrange
            DateTime findFromTime = DateTime.Today.AddDays(5);
            DateTime findToTime = DateTime.Today.AddDays(10);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=bookedTimeFrom, EndDate=bookedTimeTo, IsActive=true, CustomerId=1, RoomId=1 },
            };
            bookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            roomRepository.Setup(repo => repo.GetAll()).Returns(new List<Room> { new Room() { Id = 1 } });

            // Act
            int roomId = bookingManager.FindAvailableRoom(findFromTime, findToTime);
            // Assert
            Assert.Equal(-1, roomId);
        }

        public static IEnumerable<object[]> GetData_FindAvailableRoom_NoAviableRooms()
        {
            var data = new List<object[]>
            {
                // int day start - int day end 
                new object[] {DateTime.Today.AddDays(7), DateTime.Today.AddDays(8) },
                new object[] {DateTime.Today.AddDays(2), DateTime.Today.AddDays(11) },
                new object[] {DateTime.Today.AddDays(4), DateTime.Today.AddDays(11) },
                new object[] {DateTime.Today.AddDays(6), DateTime.Today.AddDays(5) },
                new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(10) },
            };

            return data;
        }

        [Theory]
        [MemberData(nameof(GetData_FindAvailableRoom_RoomsAviable))]
        public void FindAvailableRoom_RoomsNotBooked_NotMinusOne(DateTime bookedTimeFrom, DateTime bookedTimeTo, int bookedRoomId, int roomId, int expectedRoomId)
        {
            // Arrange
            DateTime findFromTime = DateTime.Today.AddDays(5);
            DateTime findToTime = DateTime.Today.AddDays(10);
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=bookedTimeFrom, EndDate=bookedTimeTo, IsActive=true, CustomerId=1, RoomId=bookedRoomId },
            };
            bookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            roomRepository.Setup(repo => repo.GetAll()).Returns(new List<Room> { new Room() { Id = roomId } });

            // Act
            int FoundRoomId = bookingManager.FindAvailableRoom(findFromTime, findToTime);
            // Assert
            Assert.Equal(expectedRoomId, FoundRoomId);
        }


        public static IEnumerable<object[]> GetData_FindAvailableRoom_RoomsAviable()
        {
            var data = new List<object[]>
            {
                // int day start - int day end - booked room id - room id - expected room id
                new object[] {DateTime.Today.AddDays(11), DateTime.Today.AddDays(13), 1, 1, 1 },
                new object[] {DateTime.Today.AddDays(3), DateTime.Today.AddDays(4), 4, 4, 4 },
                new object[] {DateTime.Today.AddDays(11), DateTime.Today.AddDays(12), 5, 5, 5 },
                new object[] {DateTime.Today.AddDays(1), DateTime.Today.AddDays(3), 6, 6, 6 },

            };

            return data;
        }

        #endregion

        #region Tests and Data for GetFullyOccupiedDates

        [Fact]
        public void GetFullyOccupiedDates_StartDateLaterThanEndDate_ThrowsException()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today;

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => bookingManager.GetFullyOccupiedDates(startDate, endDate));
        }

        [Fact]
        public void GetFullyOccupiedDates_NoBookings_ReturnsEmptyList()
        {
            // Arrange
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1);
            bookingRepository.Setup(repo => repo.GetAll()).Returns(new List<Booking>());

            // Act
            List<DateTime> Response = bookingManager.GetFullyOccupiedDates(startDate, endDate);

            // Assert
            Assert.Equal(new List<DateTime>(), Response);
        }

        [Theory]
        [MemberData(nameof(GetData_GetFullyOccupiedDates_bookingInactive))]
        [MemberData(nameof(GetData_GetFullyOccupiedDates_bookingOccupied))]
        [MemberData(nameof(GetData_GetFullyOccupiedDates_bookingAviable))]
        public void GetFullyOccupiedDates_bookings_ReturnsDateTimeList(DateTime bookingStart, DateTime bookingEnd, DateTime bookedStart, DateTime bookedEnd, bool active, List<DateTime> expectedResponse)
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=bookedStart, EndDate=bookedEnd, IsActive=active, CustomerId=1, RoomId=1 },
            };
            bookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            roomRepository.Setup(repo => repo.GetAll()).Returns(new List<Room> { new Room() });

            // Act
            List<DateTime> Response = bookingManager.GetFullyOccupiedDates(bookingStart, bookingEnd);

            // Assert
            Assert.Equal(expectedResponse, Response);
        }

        public static IEnumerable<object[]> GetData_GetFullyOccupiedDates_bookingInactive()
        {
            var data = new List<object[]>
            {
                // DateTime booked end - DateTime booked start- DateTime booking day start - DateTime booking  day end - booking active - List<bookings>
                new object[] {DateTime.Today.AddDays(10), DateTime.Today.AddDays(20), DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), false, new List<DateTime>()},
                new object[] { DateTime.Today.AddDays(10), DateTime.Today.AddDays(20), DateTime.Today.AddDays(5), DateTime.Today.AddDays(10), false, new List<DateTime>()},
                new object[] { DateTime.Today.AddDays(10), DateTime.Today.AddDays(20), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(0), false, new List<DateTime>()},
            };

            return data;
        }

        public static IEnumerable<object[]> GetData_GetFullyOccupiedDates_bookingOccupied()
        {
            var data = new List<object[]>
            {
                // DateTime booked end - DateTime booked start- DateTime booking day start - DateTime booking  day end - booking active - List<bookings>
                new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(10), DateTime.Today.AddDays(1), DateTime.Today.AddDays(5), true, new List<DateTime>() { DateTime.Today.AddDays(5) } },
                new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(10), DateTime.Today.AddDays(6), DateTime.Today.AddDays(6), true, new List<DateTime>() { DateTime.Today.AddDays(6) } },
                new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(10), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(6), true, new List<DateTime>() { DateTime.Today.AddDays(5), DateTime.Today.AddDays(6) } },
                new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(6), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(10), true, new List<DateTime>() { DateTime.Today.AddDays(5), DateTime.Today.AddDays(6) } },
            };

            return data;
        }

        public static IEnumerable<object[]> GetData_GetFullyOccupiedDates_bookingAviable()
        {
            var data = new List<object[]>
            {
                // DateTime booked end - DateTime booked start- DateTime booking day start - DateTime booking  day end - booking active - List<bookings>
               new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(10), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(4), true, new List<DateTime>() {} },
                new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(10), DateTime.Today.AddDays(11), DateTime.Today.AddDays(12), true, new List<DateTime>() {} },
                new object[] {DateTime.Today.AddDays(-1), DateTime.Today.AddDays(2), DateTime.Today.AddDays(3), DateTime.Today.AddDays(4), true, new List<DateTime>() {} },
                new object[] {DateTime.Today.AddDays(5), DateTime.Today.AddDays(10), DateTime.Today.AddDays(4), DateTime.Today.AddDays(4), true, new List<DateTime>() {} },
            };

            return data;
        }
        #endregion
    }
}
