using QuickCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCat.ModelView
{
    public class CustomerViewModel
    {
        public Customer customer { get; set; }
        public List<Customer> customers { get; set; }
        public List<Product> products { get; set; }
        public List<Product> cart { get; set; }
        public List<Shop> shops { get; set; }
        public string ErrorMessage { get; set; }
        public User user { get; set; }
        public Product product { get; set; }
    }
}