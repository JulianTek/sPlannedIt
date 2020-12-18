using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Interface.BLL
{
    public interface IShiftCollection : IHandler<Shift>
    {
        public string GetUserFromShift(string id);
        public List<Shift> GetShiftsFromUser(string userId);
        public string GetUserEmailFromShift(string id);
    }
}
