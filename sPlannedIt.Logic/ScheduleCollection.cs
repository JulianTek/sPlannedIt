using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface;
using sPlannedIt.Interface.BLL;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Logic
{
    public class ScheduleCollection : IScheduleCollection
    {
        private readonly IScheduleHandler _scheduleHandler;
        public ScheduleCollection(IScheduleHandler scheduleHandler)
        {
            _scheduleHandler = scheduleHandler;
        }

        public List<Schedule> GetAll()
        {
            return ModelConverter.ConvertScheduleDtoListToScheduleModelList(_scheduleHandler.GetAll());
        }

        public Schedule GetById(string id)
        {
            return ModelConverter.ConvertScheduleDtoToModel(_scheduleHandler.GetById(id));
        }

        public Schedule Create(Schedule schedule)
        {
            var sched = ModelConverter.ConvertScheduleDtoToModel(
                _scheduleHandler.Create(ModelConverter.ConvertScheduleModelToDto(schedule)));
            return sched;
        }

        public Schedule Update(Schedule schedule)
        {
            return ModelConverter.ConvertScheduleDtoToModel(
                _scheduleHandler.Update(ModelConverter.ConvertScheduleModelToDto(schedule)));
        }

        public bool Delete(string id)
        {
            return _scheduleHandler.Delete(id);
        }


        public List<Shift> GetShiftsFromSchedule(string id)
        {
            return ModelConverter.ConvertShiftDtoListToShiftModelList(_scheduleHandler.GetShiftsFromSchedule(id));
        }

        public List<Shift> GetTodaysShifts(string id, DateTime date)
        {
            return ModelConverter.ConvertShiftDtoListToShiftModelList(_scheduleHandler.GetTodaysShifts(id, date));
        }

        public List<Schedule> GetSchedulesFromCompany(string id)
        {
            return ModelConverter.ConvertScheduleDtoListToScheduleModelList(
                _scheduleHandler.GetSchedulesFromCompany(id));
        }
    }
}
