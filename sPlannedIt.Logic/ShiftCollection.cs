using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface.BLL;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Logic
{
    public class ShiftCollection : IShiftCollection
    {
        public ShiftCollection(IShiftHandler shiftHandler)
        {
            _shiftHandler = shiftHandler;
        }

        private IShiftHandler _shiftHandler;

        public List<Shift> GetAll()
        {
            return ModelConverter.ConvertShiftDtoListToShiftModelList(_shiftHandler.GetAll());
        }

        public Shift Create(Shift entity)
        {
            return ModelConverter.ConvertShiftDtoToModel(
                _shiftHandler.Create(ModelConverter.ConvertShiftModelToDto(entity)));
        }

        public Shift Update(Shift entity)
        {
            return ModelConverter.ConvertShiftDtoToModel(
                _shiftHandler.Update(ModelConverter.ConvertShiftModelToDto(entity)));
        }

        public bool Delete(string id)
        {
            return _shiftHandler.Delete(id);
        }

        public Shift GetById(string id)
        {
            return ModelConverter.ConvertShiftDtoToModel(_shiftHandler.GetById(id));
        }

        public string GetUserFromShift(string id)
        {
            return _shiftHandler.GetUserFromShift(id);
        }

        public List<Shift> GetShiftsFromUser(string userId)
        {
            return ModelConverter.ConvertShiftDtoListToShiftModelList(_shiftHandler.GetShiftsFromUser(userId));
        }

        public string GetUserEmailFromShift(string id)
        {
            return _shiftHandler.GetUserEmailFromShift(id);
        }
    }
}
