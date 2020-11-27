using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Factories;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Logic
{
    public class ShiftCollection
    {
        private readonly List<Shift> _shifts = new List<Shift>();
        public List<Shift> GetAll()
        {
            List<ShiftDTO> dtos = ShiftFactory.ShiftHandler.GetAll();
            foreach (ShiftDTO dto in dtos)
            {
                _shifts.Add(ModelConverter.ConvertShiftDtoToModel(dto));
            }

            return _shifts;
        }

        public Shift GetById(string id)
        {
            var dto = ShiftFactory.ShiftHandler.GetById(id);
            return ModelConverter.ConvertShiftDtoToModel(dto);
        }

        public bool Create(Shift shift)
        {
           return ShiftFactory.ShiftHandler.Create(ModelConverter.ConvertShiftModelToDto(shift));
        }

        public bool Update(Shift shift)
        {
           return ShiftFactory.ShiftHandler.Update(ModelConverter.ConvertShiftModelToDto(shift));
        }

        public bool Delete(string id)
        {
            return ShiftFactory.ShiftHandler.Delete(id);
        }

        public string GetUserFromShift(string id)
        {
            return ShiftFactory.ShiftHandler.GetUserFromShift(id);
        }

        public List<Shift> GetShiftsFromUser(string id)
        {
            return ModelConverter.ConvertShiftDtoListToShiftModelList(ShiftFactory.ShiftHandler.GetShiftsFromUser(id));
        }
    }
}
