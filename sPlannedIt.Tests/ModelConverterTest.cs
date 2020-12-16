using System;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Entities.Models;
using sPlannedIt.Logic;

namespace sPlannedIt.Tests
{
    [TestClass]
    public class ModelConverterTest
    {
        //This test method checks if Schedule DTOs are converted to models properly
        [TestMethod]
        public void ScheduleDtoToModelTest()
        {
            // Arrange
            var expected = new Schedule("testId", "company", "testName");
            var dto = new ScheduleDTO("testName", "testId", "company");

            // Act
            var actual = ModelConverter.ConvertScheduleDtoToModel(dto);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.CompanyId, actual.CompanyId);
            Assert.AreEqual(expected.ScheduleId, actual.ScheduleId);
        }

        //This test method does the opposite of the previous one; it checks if a schedule model is converted to a dto properly
        [TestMethod]
        public void ScheduleModelToDtoTest()
        {
            // Arrange
            var model = new Schedule("testId", "company", "testName");
            var expected = new ScheduleDTO("testName", "testId", "company");

            // Act
            var actual = ModelConverter.ConvertScheduleModelToDto(model);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.CompanyId, actual.CompanyId);
            Assert.AreEqual(expected.ScheduleId, actual.ScheduleId);
        }

        // This test method checks if a company model is converted to a DTO properly
        [TestMethod]
        public void CompanyModelToDtoTest()
        {
            // Arrange
            var model = new Company("testId", "companyName");
            var expected = new CompanyDTO("testId", "companyName");

            // Act
            var actual = ModelConverter.ConvertModelToCompanyDto(model);

            // Assert
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.CompanyId, actual.CompanyId);
        }

        // This test method checks if a company DTO is converted to a model properly
        [TestMethod]
        public void CompanyDtoToModelTest()
        {
            // Arrange
            var expected = new Company("testId", "companyName");
            var dto = new CompanyDTO("testId", "companyName");

            // Act
            var actual = ModelConverter.ConvertCompanyDtoToModel(dto);

            // Assert
            Assert.AreEqual(expected.CompanyName, actual.CompanyName);
            Assert.AreEqual(expected.CompanyId, actual.CompanyId);
        }

        // This test method checks if a shift model is converted to a dto properly
        [TestMethod]
        public void ShiftModelToDtoTest()
        {
            // Arrange
            var model = new Shift("shiftId", "scheduleId", "Julian", DateTime.Today, 12, 23);
            var expected = new ShiftDTO("shiftId", "scheduleId", "Julian", DateTime.Today, 12, 23);

            // Act
            var actual = ModelConverter.ConvertShiftModelToDto(model);

            // Assert
            Assert.AreEqual(expected.ShiftId, actual.ShiftId);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.ScheduleId, actual.ScheduleId);
            Assert.AreEqual(expected.ShiftDate, actual.ShiftDate);
            Assert.AreEqual(expected.StartTime, actual.StartTime);
            Assert.AreEqual(expected.EndTime, actual.EndTime);
        }

        [TestMethod]
        public void ShiftDtoToModelTest()
        {
            // Arrange
            var expected = new Shift("shiftId", "scheduleId", "Julian", DateTime.Today, 12, 23);
            var dto = new ShiftDTO("shiftId", "scheduleId", "Julian", DateTime.Today, 12, 23);

            // Act
            var actual = ModelConverter.ConvertShiftDtoToModel(dto);

            // Assert
            Assert.AreEqual(expected.ShiftId, actual.ShiftId);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.ScheduleId, actual.ScheduleId);
            Assert.AreEqual(expected.ShiftDate, actual.ShiftDate);
            Assert.AreEqual(expected.StartTime, actual.StartTime);
            Assert.AreEqual(expected.EndTime, actual.EndTime);
        }
    }
}
