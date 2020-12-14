using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Data
{
    public class MemCompanyHandler : ICompanyHandler
    {
        private readonly List<CompanyDTO> _companies = new List<CompanyDTO>();
        private readonly UserManager<IdentityUser> _userManager;
        public List<CompanyDTO> GetAll()
        {
            return _companies;
        }

        public bool Create(CompanyDTO entity)
        {
            var oldCount = _companies.Count;
            CompanyDTO dto = new CompanyDTO(entity.CompanyId, entity.CompanyName);
            _companies.Add(dto);
            if (oldCount != _companies.Count)
            {
                return true;
            }

            return false;
        }

        public bool Update(CompanyDTO entity)
        {
            CompanyDTO dto = _companies.FirstOrDefault(c => c.CompanyId == entity.CompanyId);
            if (dto != null)
            {
                dto.CompanyId = entity.CompanyId;
                dto.CompanyName = entity.CompanyName;
                return true;
            }

            return false;
        }

        public bool Delete(string id)
        {
            CompanyDTO dto = _companies.FirstOrDefault(c => c.CompanyId == id);
            if (dto != null)
            {
                if (_companies.Contains(dto))
                {
                    _companies.Remove(dto);
                    return true;
                }
            }

            return false;
        }

        public CompanyDTO GetById(string id)
        {
            return _companies.FirstOrDefault(c => c.CompanyId == id);
        }

        public CompanyDTO GetCompanyFromUser(string userId)
        {
            throw new NotImplementedException();
        }

        public bool AddEmployee(string userId, CompanyDTO company)
        {
            int oldCount = company.Employees.Count;
            company.Employees.Add(userId);
            if (company.Employees.Count != oldCount)
            {
                return true;
            }

            return false;
        }

        public bool RemoveEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllEmployees(string id)
        {
           CompanyDTO c = _companies.FirstOrDefault(c => c.CompanyId == id);
           return c?.Employees;
        }

        public void RemoveAllEmployees(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfCompanyNameExists(string name)
        {
            foreach (CompanyDTO c in _companies)
            {
                if (name == c.CompanyName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckIfEmployeeInCompany(string userId, string companyId)
        {
            CompanyDTO c = _companies.FirstOrDefault(c => c.CompanyId == companyId);
            {
                foreach (var id in c.Employees)
                {
                    if (id == userId)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
