using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Factories;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Logic
{
    public class ScheduleCollection
    {
        private readonly List<Schedule> _schedules = new List<Schedule>();

        public List<Schedule> GetAll()
        {
            List<ScheduleDTO> dtos = ScheduleFactory.ScheduleHandler.GetAll();
            foreach (ScheduleDTO dto in dtos)
            {
                Schedule schedule = ModelConverter.ConvertScheduleDtoToModel(dto);
                _schedules.Add(schedule);
            }

            return _schedules;
        }

        public Schedule GetSchedule(string id)
        {
            return ModelConverter.ConvertScheduleDtoToModel(ScheduleFactory.ScheduleHandler.GetById(id));
        }

        public bool Create(Schedule schedule)
        {
            ScheduleDTO dto = ModelConverter.ConvertScheduleModelToDto(schedule);
           return ScheduleFactory.ScheduleHandler.Create(dto);
        }

        public bool Update(Schedule schedule)
        {
            ScheduleDTO dto = ModelConverter.ConvertScheduleModelToDto(schedule);
           return ScheduleFactory.ScheduleHandler.Update(dto);
        }

        public bool Delete(string id)
        {
           return ScheduleFactory.ScheduleHandler.Delete(id);
        }

        public List<Shift> GetShiftsFromSchedule(string id)
        {
            List<Shift> models = new List<Shift>();
            List <ShiftDTO> dtos = ScheduleFactory.ScheduleHandler.GetShiftsFromSchedule(id);
            foreach (ShiftDTO dto in dtos)
            {
                models.Add(ModelConverter.ConvertShiftDtoToModel(dto));
            }
            return models;
        }

        public List<Schedule> GetSchedulesFromCompany(string id)
        {
            List<ScheduleDTO> dtos = ScheduleFactory.ScheduleHandler.GetSchedulesFromCompany(id);
            foreach (ScheduleDTO dto in dtos)
            {
                _schedules.Add(ModelConverter.ConvertScheduleDtoToModel(dto));
            }

            return _schedules;
        }

        public List<Shift> GetTodaysShifts(string id)
        {
            List<Shift> models = new List<Shift>();
            List<ShiftDTO> dtos =  ScheduleFactory.ScheduleHandler.GetTodaysShifts(id, DateTime.Today);
            foreach (ShiftDTO dto in dtos)
            {
                models.Add(ModelConverter.ConvertShiftDtoToModel(dto));
            }
            return models;
        }
    }
}
