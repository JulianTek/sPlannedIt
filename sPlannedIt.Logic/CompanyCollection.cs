using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Factories;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Logic
{
    public class CompanyCollection
    {
        public List<Company> _companies = new List<Company>();

        public List<Company> GetAll()
        {
            List<CompanyDTO> dtos = CompanyFactory.CompanyHandler.GetAll();
            List<Company> models = new List<Company>();
            foreach (CompanyDTO dto in dtos)
            {
                models.Add(ModelConverter.ConvertCompanyDtoToModel(dto));
            }
            _companies = models;
            return _companies;
        }

        public Company GetCompany(string id)
        {
            return ModelConverter.ConvertCompanyDtoToModel(CompanyFactory.CompanyHandler.GetById(id));
        }

        public void Create(Company company)
        {
            var dto = ModelConverter.ConvertModelToCompanyDto(company);
            CompanyFactory.CompanyHandler.Create(dto);
        }

        public bool Update(Company company)
        {
            var dto = ModelConverter.ConvertModelToCompanyDto(company);
            return CompanyFactory.CompanyHandler.Update(dto);
        }

        public bool Delete(Company company)
        {
            var dto = ModelConverter.ConvertModelToCompanyDto(company);
           return CompanyFactory.CompanyHandler.Delete(dto.CompanyId);
        }

        public Company GetCompanyFromUser(string userId)
        {
            return ModelConverter.ConvertCompanyDtoToModel(CompanyFactory.CompanyHandler.GetCompanyFromUser(userId));
        }

        public List<string> GetAllEmployees(Company company)
        {
            var dto = ModelConverter.ConvertModelToCompanyDto(company);
            List<string> userDtos = CompanyFactory.CompanyHandler.GetAllEmployees(dto.CompanyId);
            return userDtos;
        }

        public bool AddEmployee(string userId, Company company)
        {
            var companyDto = ModelConverter.ConvertModelToCompanyDto(company);
            return CompanyFactory.CompanyHandler.AddEmployee(userId, companyDto);
        }

        public bool RemoveEmployee(string id)
        {
            return CompanyFactory.CompanyHandler.RemoveEmployee(id);
        }

        public void RemoveAllEmployees(List<string> ids)
        {
            CompanyFactory.CompanyHandler.RemoveAllEmployees(ids);
        }

        public bool CheckIfEmployeeIsInCompany(Company company, string userId)
        {
            return CompanyFactory.CompanyHandler.CheckIfEmployeeInCompany(userId, company.CompanyId);
        }

        public bool CheckIfCompanyNameExists(Company company)
        {
            return CompanyFactory.CompanyHandler.CheckIfCompanyNameExists(company.CompanyName);
        }
    }
}
