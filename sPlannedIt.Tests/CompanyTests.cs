using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Tests
{
    [TestClass]
    public class CompanyTests
    {
        // This test checks if a list of companies is retrieved properly
        [TestMethod]
        public void GetAll_ReturnsList()
        {
            // Arrange
            var comp1 = new CompanyDTO("1", "tesla");
            var comp2 = new CompanyDTO("1", "apple");
            var comp3 = new CompanyDTO("1", "microsoft");
            var comp4 = new CompanyDTO("1", "nintendo");
            List<CompanyDTO> companies = new List<CompanyDTO>{comp1, comp2, comp3, comp4};

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.GetAll()).Returns(companies);

            // Act
            var actual = mock.Object.GetAll();

            // Assert
            CollectionAssert.AreEquivalent(companies, actual);
        }

        // This method checks if a company is retriever properly by its id
        [TestMethod]
        public void GetById_ReturnsEntity()
        {
            // Arrange
            var id = "test";
            var name = "hp";
            var company = new CompanyDTO("test", "hp");

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.GetById(id)).Returns(company);

            // Act
            var actual = mock.Object.GetById("test");

            // Assert
            Assert.AreSame(company, actual);
            Assert.AreEqual(name, actual.CompanyName);
        }

        // This test checks that when an id that doesnt exist is inserted, a null object is returned
        [TestMethod]
        public void GettingInvalidEntity_ReturnsNull()
        {
            // Arrange
            CompanyDTO comp = new CompanyDTO("1", "schlatt transportation co.");

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.GetById("1")).Returns(comp);

            // Act
            var actual = mock.Object.GetById("2");

            // Assert
            Assert.IsNull(actual);
        }

        // This method checks that the created entity is also returned
        [TestMethod]
        public void Creating_ReturnsEntity()
        {
            var id = "id";
            var name = "lego";
            CompanyDTO company = new CompanyDTO(id, name);

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.Create(company)).Returns(company);

            // Act
            var actual = mock.Object.Create(company);

            // Assert
            Assert.AreSame(company, actual);
        }

        // This method checks that when an invalid entity is sent as parameter, it does not get created and null is returned
        [TestMethod]
        public void CreatingInvalidEntity_ReturnsNull()
        {
            // Arrange
            var comp = new CompanyDTO("id", "lakon");
            var invalid = new CompanyDTO(null, null);

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.Create(comp)).Returns(comp);

            // Act
            var actual = mock.Object.Create(invalid);

            // Assert
            Assert.IsNull(actual);
        }

        // This method checks that when a valid employee is inserted as parameter, the proper company is returned
        [TestMethod]
        public void GettingCompanyFromUser_ReturnsCompany()
        {
            // Arrange
            var userId = "schlatt";
            var company = new CompanyDTO("id", "schlatt transportation co.");

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.GetCompanyFromUser(userId)).Returns(company);

            // Act
            var actual = mock.Object.GetCompanyFromUser(userId);

            // Assert
            Assert.AreSame(company, actual);
            Assert.AreEqual(company.CompanyId, actual.CompanyId);
            Assert.AreEqual(company.CompanyName, actual.CompanyName);
        }

        // This method checks that if a valid employee property is inserted, true is returned
        [TestMethod]
        public void AddingEmployeeSucessfully_ReturnsTrue()
        {
            var id = "idEmployee";
            var comp = new CompanyDTO("idCompany", "Capitalism inc.");

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.AddEmployee(id, comp)).Returns(true);

            // Act
            var actual = mock.Object.AddEmployee(id, comp);

            // Assert
            Assert.AreEqual(true, actual);
        }

        // This method checks the opposite; when an invalid property is inserted, false is returned
        [TestMethod]
        public void AddingInvalidEmployee_ReturnsFalse()
        {
            // Arrange
            var id = "idd";
            var company = new CompanyDTO("id", "valve");

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.AddEmployee(id, company)).Returns(true);
            
            // Act
            var actual = mock.Object.AddEmployee(null, company);

            // Assert
            Assert.AreEqual(false, actual);
        }

        // This method checks that sucessfully removing an employee returns true
        [TestMethod]
        public void RemovingEmployee_ReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.RemoveEmployee(id)).Returns(true);

            // Act
            var actual = mock.Object.RemoveEmployee(id);

            // Assert
            Assert.AreEqual(true, actual);
        }

        // This method checks that when an invalid id is inserted, false is returned
        [TestMethod]
        public void RemovingInvalidEmployee_ReturnsFalse()
        {
            const string invalidId = "no";
            var validId = Guid.NewGuid().ToString();

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.RemoveEmployee(validId)).Returns(true);

            // Act
            var actual = mock.Object.RemoveEmployee(invalidId);

            // Assert
            Assert.AreEqual(false, actual);
        }

        // This method checks that getting all employees returns a proper list
        [TestMethod]
        public void GettingAllEmployees_ReturnsProperList()
        {
            const string emp1 = "carson king";
            const string emp2 = "johnathan schlatt";
            const string emp3 = "ted nivison";
            var companyId = Guid.NewGuid().ToString();
            var company = new CompanyDTO(companyId, "Lunch Club");
            List<string> employees = new List<string>{emp1, emp2, emp3};

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.GetAllEmployees(companyId)).Returns(employees);

            // Act
            var actual = mock.Object.GetAllEmployees(company.CompanyId);

            // Assert
            CollectionAssert.AreEquivalent(employees, actual);
        }

        // This method checks that when a name that already exists is inserted, the method returns true
        [TestMethod]
        public void CheckingCompNameThatAlreadyExists_ReturnsTrue()
        {
            // Arrange
            var name = "schlatt co.";

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.CheckIfCompanyNameExists(name)).Returns(true);

            // Act
            var actual = mock.Object.CheckIfCompanyNameExists("schlatt co.");

            // Assert
            Assert.AreEqual(true, actual);
        }

        // This method checks the opposite; when a name that doesn't exist is inserted, false is returned
        [TestMethod]
        public void CheckingCompNameThatDoesntExist_ReturnsFalse()
        {
            // Arrange
            var name = "schlatt co.";

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.CheckIfCompanyNameExists(name)).Returns(true);

            // Act
            var actual = mock.Object.CheckIfCompanyNameExists("capitalism inc.");

            // Assert
            Assert.AreEqual(false, actual);
        }

        // This method checks that when an employee that is in a company is inserted, true is returned
        [TestMethod]
        public void CheckingIfEmployeeIsInCompany_ReturnsTrue()
        {
            var userId = Guid.NewGuid().ToString();
            var companyId = Guid.NewGuid().ToString();

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.CheckIfEmployeeInCompany(userId, companyId)).Returns(true);

            // Act
            var actual = mock.Object.CheckIfEmployeeInCompany(userId, companyId);

            // Assert
            Assert.AreEqual(true, actual);
        }

        // This method checks that when an employee not in the company is inserted, false is returned
        [TestMethod]
        public void CheckingIfInvalidEmployeeIsInCompany_ReturnsFalse()
        {
            var userId = Guid.NewGuid().ToString();
            var invalid = "hello";
            var companyId = Guid.NewGuid().ToString();

               // Mock the interface and its implementation
            var mock = new Mock<ICompanyHandler>();
            mock.Setup(x => x.CheckIfEmployeeInCompany(userId, companyId)).Returns(true);

            // Act
            var actual = mock.Object.CheckIfEmployeeInCompany(invalid, companyId);

            // Assert
            Assert.AreEqual(false, actual);
        }
    }
}
