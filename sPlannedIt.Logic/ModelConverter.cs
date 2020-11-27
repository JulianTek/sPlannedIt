using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Logic
{
    public static class ModelConverter
    {
        private static Company _company;
        private static CompanyDTO _companyDto;
        private static Schedule _schedule;
        private static ScheduleDTO _scheduleDto;
        private static Shift _shift;
        private static ShiftDTO _shiftDto;

        public static Company ConvertCompanyDtoToModel(CompanyDTO dto)
        {
            _company = new Company()
            {
                CompanyID = dto.CompanyID,
                CompanyName = dto.CompanyName,
                Employees = dto.Employees
            };
            return _company;
        }

        public static CompanyDTO ConvertModelToCompanyDto(Company company)
        {
            _companyDto = new CompanyDTO()
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName,
                Employees = company.Employees
            };
            return _companyDto;
        }

        public static Schedule ConvertScheduleDtoToModel(ScheduleDTO dto)
        {
            _schedule = new Schedule()
            { 
                CompanyID = dto.CompanyID,
                Name = dto.Name,
                ScheduleID = dto.ScheduleID,
                //todo: implement getting shifts
            };
            return _schedule;
        }

        public static ScheduleDTO ConvertScheduleModelToDto(Schedule schedule)
        {
            _scheduleDto = new ScheduleDTO()
            {
                CompanyID = schedule.CompanyID,
                Name = schedule.Name,
                ScheduleID = schedule.ScheduleID
            };
            return _scheduleDto;
        }

        public static Shift ConvertShiftDtoToModel(ShiftDTO dto)
        {
            _shift = new Shift()
            {
                ScheduleID = dto.ScheduleID,
                ShiftID = dto.ShiftID,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                ShiftDate = dto.ShiftDate,
                UserID = dto.UserID
            };
            return _shift;
        }

        public static ShiftDTO ConvertShiftModelToDto(Shift shift)
        {
            _shiftDto = new ShiftDTO()
            {
                ScheduleID = shift.ScheduleID,
                ShiftID = shift.ShiftID,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                ShiftDate = shift.ShiftDate,
                UserID = shift.UserID
            };
            return _shiftDto;
        }

        public static List<Shift> ConvertShiftDtoListToShiftModelList(List<ShiftDTO> dtos)
        {
            List<Shift> models = new List<Shift>();
            foreach (ShiftDTO dto in dtos)
            {
                models.Add(ConvertShiftDtoToModel(dto));
            }

            return models;
        }
    }
}
