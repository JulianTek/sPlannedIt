﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Interface;

namespace sPlannedIt.Logic.Models
{
    public class Shift : IShift
    {
        public Shift(string userId, DateTime shiftDate, int startTime, int endTime, string scheduleId)
        {
            ShiftID = Guid.NewGuid().ToString();
            ScheduleID = scheduleId;
            UserID = userId;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Shift(string shiftId, string scheduleId, string userId, DateTime shiftDate, int startTime, int endTime)
        {
            ShiftID = shiftId;
            ScheduleID = scheduleId;
            UserID = userId;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Shift()
        {
            
        }

        public string ShiftID { get; set; }
        public string ScheduleID { get; set; }
        public string UserID { get; set; }
        public DateTime ShiftDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }


        public IShift UpdateShift(string id, string userId, DateTime shiftDate, int startTime, int endTime)
        {
            UserID = userId;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
            return this;
        }
    }
}
