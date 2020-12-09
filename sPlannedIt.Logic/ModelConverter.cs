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
            _company = new Company(dto.CompanyId, dto.CompanyName, dto.Employees);
            return _company;
        }

        public static CompanyDTO ConvertModelToCompanyDto(Company company)
        {
            _companyDto = new CompanyDTO(company.CompanyId, company.CompanyName, company.Employees);
            return _companyDto;
        }

        public static Schedule ConvertScheduleDtoToModel(ScheduleDTO dto)
        {
            _schedule = new Schedule(dto.ScheduleId, dto.CompanyId, dto.Name);
            return _schedule;
        }

        public static ScheduleDTO ConvertScheduleModelToDto(Schedule schedule)
        {
            _scheduleDto = new ScheduleDTO(schedule.Name, schedule.ScheduleId, schedule.CompanyId);

            return _scheduleDto;
        }

        public static Shift ConvertShiftDtoToModel(ShiftDTO dto)
        {
            _shift = new Shift(dto.ShiftId, dto.ScheduleId, dto.UserId, dto.ShiftDate, dto.StartTime, dto.EndTime);
            return _shift;
        }

        public static ShiftDTO ConvertShiftModelToDto(Shift shift)
        {
            _shiftDto = new ShiftDTO(shift.ShiftId, shift.ScheduleId, shift.UserId, shift.ShiftDate, shift.StartTime, shift.EndTime);
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
