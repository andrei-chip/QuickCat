using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCat.Models
{
    public class Shop
    {
        public int ID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string OpenHoursWeek { get; set; }
        public string OpenHoursWeekend { get; set; }
    }
}