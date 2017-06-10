using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLA.Models
{
    public class Helper
    {
        public int GetAllSaturday(int year)
        {
            List<DateTime> Dates = new List<DateTime>();


            DateTime Date = new DateTime(year, 1, 1);

            int count = 0;
            while (Date.Year == year)
            {
                if ((Date.DayOfWeek == DayOfWeek.Saturday))
                    count++;
                Date = Date.AddDays(1);
            }
            return count;
        }

        public int GetAllSunday(int year)
        {
            List<DateTime> Dates = new List<DateTime>();


            DateTime Date = new DateTime(year, 1, 1);

            int count = 0;
            while (Date.Year == year)
            {
                if ((Date.DayOfWeek == DayOfWeek.Sunday))
                    count++;
                Date = Date.AddDays(1);
            }
            return count;
        }
    }
}