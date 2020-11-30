using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Data;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Factories
{
    public class ShiftFactory
    {
        private static IShiftHandler _shiftHandler;

        public static IShiftHandler ShiftHandler
        {
            get
            {
                if (_shiftHandler == null)
                {
                    _shiftHandler = new ShiftHandler();
                }
                return _shiftHandler;
            }
        }
    }
}
