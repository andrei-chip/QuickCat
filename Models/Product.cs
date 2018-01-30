using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCat.Models
{
    public class Product
    {
        [Required]
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductCategory { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "must be float number")]
        
        public double ProductPrice { get; set; }
        [Required]

        public string ProductText { get; set; }
        public string ProductImg { get; set; }
        [Required]
        public string PetType { get; set; }
    }
}