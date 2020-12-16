using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sPlannedIt.Data;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface;
using sPlannedIt.Logic;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Tests
{
    [TestClass]
    public class ScheduleTests
    {
        // Mocking the neccessary interfaces here
        private readonly ScheduleCollection _schedCol;
        private readonly Mock<IScheduleHandler> _mockHandler = new Mock<IScheduleHandler>();

        public ScheduleTests()
        {
            _schedCol = new ScheduleCollection(_mockHandler.Object);
        }

            // Unit test to get all schedules
            [TestMethod]
        public void GetAll_ReturnsList()
        {
            // Arrange
               // Setting up the expected dto list
            ScheduleDTO dto1 = new ScheduleDTO("1", "1", "1");
            ScheduleDTO dto2 = new ScheduleDTO("2", "2", "2");
            ScheduleDTO dto3 = new ScheduleDTO("3", "3", "3");
            List<ScheduleDTO> dtos = new List<ScheduleDTO>() {dto1, dto2, dto3};

               // Mocking the repo and implementation
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.GetAll()).Returns(dtos);

            // Act
            var actual = _schedCol.GetAll();

            // Assert
            CollectionAssert.AreEquivalent(dtos, actual);


        }

        // Unit test to find specific schedule from id
        [TestMethod]
        public void FindingSchedule_ReturnsCorrectEntity()
        {
            // Arrange
            var id = "testId";
            var companyId = "tesla";
            var name = "hello world";

               // Create the expected object
            var expected = new ScheduleDTO(name, id, companyId);

               // Mock the interface and expected result
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.GetById(id)).Returns(expected);

            // Act
            var actual = mock.Object.GetById(id);

            // Assert
            Assert.AreSame(expected, actual);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        // Unit test for creating a schedule, checks if the schedule is returned
        [TestMethod]
        public void CreatingSchedule_ReturnsSchedule()
        {
            var id = "beans";
            var companyId = "nintendo";
            var name = "super mario";
            var schedule = new ScheduleDTO(name, id, companyId);


            // Mock the interface and implementation
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.Create(schedule)).Returns(schedule);

            // Act
            var actual = mock.Object.Create(schedule);

            // Assert
            Assert.AreSame(schedule, actual);
        }

        // This method checks that when an invalid entity is sent, null is returned
        [TestMethod]
        public void CreatingInvalidSchedule_ReturnsNull()
        {
            var id = "beans";
            var companyId = "nintendo";
            var name = "super mario";
            var schedule = new ScheduleDTO(name, id, companyId);
            var invalidSchedule = new ScheduleDTO(null, null, null);


            // Mock the interface and implementation
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.Create(schedule)).Returns(schedule);

            // Act
            var actual = mock.Object.Create(invalidSchedule);

            // Assert
            Assert.IsNull(actual);
        }

        // This unit test checks whether an updated schedule is actually updated and not the old one
        [TestMethod]
        public void UpdatingSchedule_ReturnsNewSchedule()
        {
            // Arrange
            var old = new ScheduleDTO("old", "xyz", "abc");
            var newName = "def";
            var newCompany = "ghi";
            var newId = "jkl";
            var newSched = new ScheduleDTO(newName, newId, newCompany);

               // Mock the interface and implementation
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.Update(newSched)).Returns(newSched);

            // Act
            var actual = mock.Object.Update(newSched);

            // Assert
               // Asserting that the value is actually updated and is not the old schedule
            Assert.AreSame(newSched, actual);
            Assert.AreNotSame(old, actual);
        }

        // This test checks whether a schedule gets deleted, and whether the correct schedule gets deleted
        [TestMethod]
        public void DeletingSchedule_ReturnsTrue()
        {
            // Arrange
            ScheduleDTO toDelete = new ScheduleDTO("delete this", "4", "4");

            // Mock the interface and implementation (including one for getting all schedules)
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.Delete("4")).Returns(true);

            // Act
            var actual = mock.Object.Delete("4");

            // Assert
               // Assert that a deletion was made
            Assert.AreEqual(true, actual);
        }

        // This unit test tests whether all shifts are properly obtained
        [TestMethod]
        public void GetShiftsFromSchedule_ReturnsProperList()
        {
            // Arrange
            var sched = new ScheduleDTO("hello", "test", "company");
            var shift1 = new ShiftDTO("1", "test", "user", DateTime.Now, 1, 2);
            var shift2 = new ShiftDTO("2", "test", "user", DateTime.Now, 1, 2);
            var shift3 = new ShiftDTO("3", "test", "user", DateTime.Now, 1, 2);
            var shift4 = new ShiftDTO("4", "test", "user", DateTime.Now, 1, 2);
            var dtos = new List<ShiftDTO>() {shift1, shift2, shift3, shift4};

               // Mock the interface and implementation
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.GetShiftsFromSchedule("test")).Returns(dtos);

            // Act
               // I am using sched.scheduleId to check if it actually gets the id properly
            var actual = mock.Object.GetShiftsFromSchedule(sched.ScheduleId);

            // Assert
            CollectionAssert.AreEquivalent(dtos, actual);
        }

        // This unit test tests for getting a company's schedule
        [TestMethod]
        public void GetSchedulesFromCompany_ReturnsAllSchedules()
        {
            // Arrange
               // Creating a company whose id i will send to the mock interface
            var company = new CompanyDTO("id", "nintendo");

               // Creating the schedules, some of which do not belong to the company
            var dto1 = new ScheduleDTO("1", "1", "id");
            var dto2 = new ScheduleDTO("2", "2", "id");
            var dto3 = new ScheduleDTO("3", "3", "id");
            var dto4 = new ScheduleDTO("4", "4", "id");
            var dto5 = new ScheduleDTO("5", "5", "id1");
            var dto6 = new ScheduleDTO("6", "6", "id1");
            var dto7 = new ScheduleDTO("7", "7", "id1");
            var dto8 = new ScheduleDTO("8", "8", "id1");
               // Creating 2 lists; one with all schedules, one with all schedules belonging to the company
            List<ScheduleDTO> allDtos = new List<ScheduleDTO>() {dto1, dto2, dto3, dto4, dto5, dto6, dto7, dto8};
            List<ScheduleDTO> companyScheduleDtos = new List<ScheduleDTO>() {dto1, dto2, dto3, dto4};

               // Mocking the interface and its implementation
            var mock = new Mock<IScheduleHandler>();
            mock.Setup(x => x.GetSchedulesFromCompany("id")).Returns(companyScheduleDtos);

            // Act
            var actual = mock.Object.GetSchedulesFromCompany(company.CompanyId);

            // Assert
               // Asserts that the company's schedules were retrieved
            CollectionAssert.AreEquivalent(companyScheduleDtos, actual);
               // Asserts that none of the other dtos are in the list
            CollectionAssert.AreNotEquivalent(allDtos, actual);
        }
    }
}
