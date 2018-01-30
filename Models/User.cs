using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCat.Models
{
    public class User
    {
        [Required]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",ErrorMessage ="check email")]
        public string Email { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "must be between 6 to 12 charachters")]
        public string Password { get; set; }
        public string Permission { get; set; }
        public string ErrorMessage { get; set; }
    }
}