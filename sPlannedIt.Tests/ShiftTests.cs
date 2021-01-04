using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface.BLL;
using sPlannedIt.Interface.DAL;
using sPlannedIt.Logic;

namespace sPlannedIt.Tests
{
    [TestClass]
    public class ShiftTests
    {
        private readonly ShiftCollection _collection;
        private readonly Mock<IShiftHandler> _mockHandler = new Mock<IShiftHandler>();

        public ShiftTests()
        {
            _collection = new ShiftCollection(_mockHandler.Object);
        }

        // This test method checks that when GetAll is called, every single shift will be called
        [TestMethod]
        public void GettingAllShifts_ReturnsEveryShift()
        {
            var shift1 = new ShiftDTO("1", "1", "1,", DateTime.Today, 1, 2);
            var shift2 = new ShiftDTO("2", "1", "2,", DateTime.Today, 1, 2);
            var shift3 = new ShiftDTO("3", "3", "3,", DateTime.Today, 1, 2);
            var shift4 = new ShiftDTO("4", "3", "4,", DateTime.Today, 1, 2);
            List<ShiftDTO> allShiftDtos = new List<ShiftDTO> { shift1, shift2, shift3, shift4 };
            List<ShiftDTO> allShiftsFromSchedule1 = new List<ShiftDTO> { shift1, shift2 };

            // Mock the interface and implementation
            var mock = new Mock<IShiftHandler>();
            _mockHandler.Setup(x => x.GetAll()).Returns(allShiftDtos);

            // Act
            var actual = _collection.GetAll();

            // Assert
            // Assert that obtained list is every shift, not just from one schedule
            for (int i = 0; i < allShiftDtos.Count; i++)
            {
                Assert.AreEqual(allShiftDtos[i].ScheduleId, actual[i].ScheduleId);
                Assert.AreEqual(allShiftDtos[i].ShiftId, actual[i].ShiftId);
                Assert.AreEqual(allShiftDtos[i].UserId, actual[i].UserId);
                Assert.AreEqual(allShiftDtos[i].ShiftDate, actual[i].ShiftDate);
                Assert.AreEqual(allShiftDtos[i].StartTime, actual[i].StartTime);
                Assert.AreEqual(allShiftDtos[i].EndTime, actual[i].EndTime);
            }
        }

        // This method checks that the correct shift is obtained
        [TestMethod]
        public void GetById_ReturnsEntity()
        {
            // Arrange
            var correctShift = new ShiftDTO("1", "1", "1", DateTime.Now, 1, 2);

            // Mock the interface and implementation
            _mockHandler.Setup(x => x.GetById("1")).Returns(correctShift);

            // Act
            var actual = _collection.GetById("1");

            // Assert
            Assert.AreEqual(correctShift.ScheduleId, actual.ScheduleId);
            Assert.AreEqual(correctShift.ShiftId, actual.ShiftId);
            Assert.AreEqual(correctShift.UserId, actual.UserId);
            Assert.AreEqual(correctShift.ShiftDate, actual.ShiftDate);
            Assert.AreEqual(correctShift.StartTime, actual.StartTime);
            Assert.AreEqual(correctShift.EndTime, actual.EndTime);
        }

        // This method checks that when a nonexisting id is called, null is returned
        [TestMethod]
        public void GetById_WithIncorrectId_ReturnsNull()
        {
            // Arrange
            var correctShift = new ShiftDTO("1", "1", "1", DateTime.Now, 1, 2);
            var exceptionCaught = false;

            // Mock the interface and implementation
            _mockHandler.Setup(x => x.GetById("1")).Returns(correctShift);

            // Act
            try
            {
                var actual = _collection.GetById("2");
            }
            catch (NullReferenceException ex)
            {
                exceptionCaught = true;
            }

            // Assert
            Assert.AreEqual(true, exceptionCaught);
        }

        // This method checks that an updated entity returns the new entity
        [TestMethod]
        public void UpdatingEntity_ReturnsNewEntity()
        {
            // Arrange
            var oldEntity = new ShiftDTO("1", "1", "1", DateTime.Now, 1, 2);
            var newEntity = new ShiftDTO("1", "2", "2", DateTime.Now, 3, 4);
            var newEntityModel = new Shift("1", "2", "2", DateTime.Now, 3, 4);

            // Mock the interface and its implementation
            var mock = new Mock<IShiftHandler>();
            _mockHandler.Setup(x => x.Update(It.IsAny<ShiftDTO>())).Returns(newEntity);

            // Act
            var actual = _collection.Update(newEntityModel);

            // Assert
            Assert.AreEqual(newEntity.ScheduleId, actual.ScheduleId);
        }

        // This method checks that if a valid id is passed, the entity will be deleted
        [TestMethod]
        public void DeletingValidEntity_ReturnsTrue()
        {
            // Arrange
            var shift = new ShiftDTO("1234", "1", "1", DateTime.MaxValue, 1, 2);

            // Mock the interface and the implementation
            _mockHandler.Setup(x => x.Delete(shift.ShiftId)).Returns(true);

            // Act
            var actual = _collection.Delete("1234");

            // Assert
            Assert.AreEqual(true, actual);
        }

        // This method checks that when an invalid result is inserted, false is returned
        [TestMethod]
        public void DeletingInvalidEntity_ReturnsFalse()
        {
            // Arrange
            var shift = new ShiftDTO("1234", "1", "1", DateTime.MaxValue, 1, 2);

            // Mock the interface and the implementation
            var mock = new Mock<IShiftHandler>();
            _mockHandler.Setup(x => x.Delete(shift.ShiftId)).Returns(true);

            // Act
            var actual = _collection.Delete("123");

            // Assert
            Assert.AreEqual(false, actual);
        }

        // This method checks that a given shift will retrieve the correct user
        [TestMethod]
        public void GettingUserFromShift_ReturnsUserId()
        {
            // Arrange
            var correctId = "schlatt";
            var incorrectId = "schlagg";
            var shift = new ShiftDTO("shift", "schedule", "schlatt", DateTime.Today, 1, 2);

            // Mock the interface and its implementation
            var mock = new Mock<IShiftHandler>();
            _mockHandler.Setup(x => x.GetUserFromShift(shift.ShiftId)).Returns(correctId);

            // Act
            var actual = _collection.GetUserFromShift("shift");

            // Assert
            Assert.AreEqual(correctId, actual);
            Assert.AreNotEqual(incorrectId, actual);
        }

        // This method checks that if an invalid id is given, null will be returned
        [TestMethod]
        public void GettingUserFromInvalidShift_ReturnsNull()
        {
            // Arrange
            var correctId = "schlatt";
            var shift = new ShiftDTO("shift", "schedule", "schlatt", DateTime.Today, 1, 2);

            // Mock the interface and its implementation
            var mock = new Mock<IShiftHandler>();
            _mockHandler.Setup(x => x.GetUserFromShift(shift.ShiftId)).Returns(correctId);

            // Act
            var actual = _collection.GetUserFromShift("shift1");

            // Assert
            Assert.IsNull(actual);
        }

        // This method checks that when a valid userId is put in, a list of shifts is returned
        [TestMethod]
        public void GettingShiftsFromValidUser_ReturnsListOfShift()
        {
            var userId = "24";
            var shift1 = new ShiftDTO("1", "1", "24", DateTime.Now, 1, 2);
            var shift2 = new ShiftDTO("2", "2", "24", DateTime.Now, 1, 2);
            var shift3 = new ShiftDTO("3", "3", "24", DateTime.Now, 1, 2);

            List<ShiftDTO> correctDtos = new List<ShiftDTO> { shift1, shift2, shift3 };

            // Mock the interface and its implementation
            var mock = new Mock<IShiftHandler>();
            _mockHandler.Setup(x => x.GetShiftsFromUser(userId)).Returns(correctDtos);

            // Act
            var actual = _collection.GetShiftsFromUser("24");

            // Assert
            for (int i = 0; i < correctDtos.Count; i++)
            {
                Assert.AreEqual(correctDtos[i].ScheduleId, actual[i].ScheduleId);
                Assert.AreEqual(correctDtos[i].ShiftId, actual[i].ShiftId);
                Assert.AreEqual(correctDtos[i].UserId, actual[i].UserId);
                Assert.AreEqual(correctDtos[i].ShiftDate, actual[i].ShiftDate);
                Assert.AreEqual(correctDtos[i].StartTime, actual[i].StartTime);
                Assert.AreEqual(correctDtos[i].EndTime, actual[i].EndTime);
            }
        }
    }
}
