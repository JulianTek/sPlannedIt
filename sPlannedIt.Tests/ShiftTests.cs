using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Tests
{
    [TestClass]
    public class ShiftTests
    {
        // This test method checks that when GetAll is called, every single shift will be called
        [TestMethod]
        public void GettingAllShifts_ReturnsEveryShift()
        {
            var shift1 = new ShiftDTO("1", "1", "1,", DateTime.Today, 1, 2);
            var shift2 = new ShiftDTO("2", "1", "2,", DateTime.Today, 1, 2);
            var shift3 = new ShiftDTO("3", "3", "3,", DateTime.Today, 1, 2);
            var shift4 = new ShiftDTO("4", "3", "4,", DateTime.Today, 1, 2);
            List<ShiftDTO> allShiftDtos = new List<ShiftDTO>{shift1, shift2, shift3, shift4};
            List<ShiftDTO> allShiftsFromSchedule1 = new List<ShiftDTO>{shift1, shift2};

               // Mock the interface and implementation
            var mock = new Mock<IShiftHandler>();
            mock.Setup(x => x.GetAll()).Returns(allShiftDtos);

            // Act
            var actual = mock.Object.GetAll();

            // Assert
               // Assert that obtained list is every shift, not just from one schedule
            CollectionAssert.AreEquivalent(allShiftDtos, actual);
            CollectionAssert.AreNotEqual(allShiftsFromSchedule1, actual);
        }

        // This method checks that the correct shift is obtained
        [TestMethod]
        public void GetById_ReturnsEntity()
        {
            // Arrange
            var correctShift = new ShiftDTO("1", "1", "1", DateTime.Now, 1, 2);
            var incorrectShift = new ShiftDTO("2", "2", "2", DateTime.Now, 1, 2);

               // Mock the interface and implementation
            var mock = new Mock<IShiftHandler>();
            mock.Setup(x => x.GetById("1")).Returns(correctShift);

            // Act
            var actual = mock.Object.GetById("1");

            // Assert
            Assert.AreSame(correctShift, actual);
            Assert.AreNotSame(incorrectShift, actual);
        }

        // This method checks that when a nonexisting id is called, null is returned
        [TestMethod]
        public void GetById_WithIncorrectId_ReturnsNull()
        {
            // Arrange
            var correctShift = new ShiftDTO("1", "1", "1", DateTime.Now, 1, 2);
            var incorrectShift = new ShiftDTO("2", "2", "2", DateTime.Now, 1, 2);

            // Mock the interface and implementation
            var mock = new Mock<IShiftHandler>();
            mock.Setup(x => x.GetById("1")).Returns(correctShift);

            // Act
            var actual = mock.Object.GetById("4");

            // Assert
            Assert.IsNull(actual);
        }
        
        // This method checks that an updated entity returns the new entity
        [TestMethod]
        public void UpdatingEntity_ReturnsNewEntity()
        {
            // Arrange
            var oldEntity = new ShiftDTO("1", "1", "1", DateTime.Now, 1, 2 );
            var newEntity = new ShiftDTO("1", "2", "2", DateTime.Now, 3, 4);

               // Mock the interface and its implementation
            var mock = new Mock<IShiftHandler>();
            mock.Setup(x => x.Update(newEntity)).Returns(newEntity);

            // Act
            var actual = mock.Object.Update(newEntity);

            // Assert
            Assert.AreSame(newEntity, actual);
            Assert.AreNotSame(oldEntity, actual);
        }

        // This method checks that if a valid id is passed, the entity will be deleted
        [TestMethod]
        public void DeletingValidEntity_ReturnsTrue()
        {
            // Arrange
            var shift = new ShiftDTO("1234", "1", "1", DateTime.MaxValue, 1, 2);

               // Mock the interface and the implementation
            var mock = new Mock<IShiftHandler>();
            mock.Setup(x => x.Delete(shift.ShiftId)).Returns(true);

            // Act
            var actual = mock.Object.Delete("1234");

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
            mock.Setup(x => x.Delete(shift.ShiftId)).Returns(true);

            // Act
            var actual = mock.Object.Delete("123");

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
            mock.Setup(x => x.GetUserFromShift(shift.ShiftId)).Returns(correctId);

            // Act
            var actual = mock.Object.GetUserFromShift("shift");

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
            mock.Setup(x => x.GetUserFromShift(shift.ShiftId)).Returns(correctId);

            // Act
            var actual = mock.Object.GetUserFromShift("shift1");

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
            var shift4 = new ShiftDTO("1", "1", "12", DateTime.Now, 1, 2);
            var shift5 = new ShiftDTO("4", "4", "12", DateTime.Now, 1, 2);
            var shift6 = new ShiftDTO("5", "5", "12", DateTime.Now, 1, 2);

            List<ShiftDTO> allDtos = new List<ShiftDTO>{shift1, shift2, shift3, shift4, shift5, shift6};
            List<ShiftDTO> correctDtos = new List<ShiftDTO>{shift1, shift2, shift3};

               // Mock the interface and its implementation
            var mock = new Mock<IShiftHandler>();
            mock.Setup(x => x.GetShiftsFromUser(userId)).Returns(correctDtos);

            // Act
            var actual = mock.Object.GetShiftsFromUser("24");

            // Assert
            CollectionAssert.AreEquivalent(correctDtos, actual);
            CollectionAssert.AreNotEquivalent(allDtos, actual);
        }
    }
}
