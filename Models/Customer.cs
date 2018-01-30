using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCat.Models
{
    public class Customer
    {
        [Required]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "must be 9 numbers (Example: 012345678)")]
        [Key]
        public string ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "check email")]
        public string Email { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "must be between 6 to 12 charachters")]
        public string Password { get; set; }
        public int Permission { get; set; }
    }
}