using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCat.Models
{
    public class Order
    {
        [Key]
        public int Number { get; set; }
        public int OrderID { get; set; }

        public string Email { get; set; }

        public int ProductID { get; set; }

        public DateTime Date { get; set; }
    }
}