using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Data;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Factories
{
    public class ScheduleFactory
    {
        private static IScheduleHandler _scheduleHandler;

        public static IScheduleHandler ScheduleHandler
        {
            get
            {
                if (_scheduleHandler == null)
                {
                    _scheduleHandler = new ScheduleHandler();
                }

                return _scheduleHandler;
            }
        }
    }
}
