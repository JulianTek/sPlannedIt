using sPlannedIt.Entities.DTOs;

namespace sPlannedIt.Interface.DAL
{
    public interface IShiftHandler : IHandler<ShiftDTO>
    {
        public string GetUserFromShift(string id);
    }
}
