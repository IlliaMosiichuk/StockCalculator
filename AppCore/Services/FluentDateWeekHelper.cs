using AppCore.Interfaces;
using FluentDate;
using FluentDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    //Stock Market is open only from Monday to Friday
    public class FluentDateWeekHelper : IWeekHelper
    {
        public DateTime GetLastWeekStartDate()
        {
            var lastWeekDate = 1.Weeks().Ago().Date;
            var mondayDate = lastWeekDate.Previous(DayOfWeek.Monday);
            return mondayDate;
        }

        public DateTime GetLastWeekEndDate()
        {
            var fridayDate = GetLastWeekStartDate().Next(DayOfWeek.Friday);
            return fridayDate;
        }
    }
}
