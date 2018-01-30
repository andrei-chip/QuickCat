using QuickCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCat.ModelView
{
    public class AdminViewModel
    {
        public CustomerViewModel cvm { get; set; }
        public List<Product> products { get; set; }
       
    }
}