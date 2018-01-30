using QuickCat.Dal;
using QuickCat.Models;
using QuickCat.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuickCat.Controllers
{
    public class HomeController : Controller
    {
        //CustomerViewModel cvm = new CustomerViewModel();
        public ActionResult Index(CustomerViewModel cvm)
        {
            return View(Session["cvm"]);
        }
        public ActionResult Index2(CustomerViewModel cvm)
        {
            
            return View("Index", Session["cvm"]);
        }

        public ActionResult About()
        {
            ShopDal shdal = new ShopDal();
            CustomerViewModel cvm = new CustomerViewModel();
            cvm.shops = shdal.Shops.ToList<Shop>();

            return View(cvm);
        }

        public ActionResult Contact(CustomerViewModel cvm)
        {
            ViewBag.Message = "Your contact page.";

            return View(Session["cvm"]);
        }
        public ActionResult Products()
        {
            CustomerViewModel cvm = null;
            if ((Session["cvm"] as CustomerViewModel) == null)
                cvm = new CustomerViewModel();
            else
                cvm = Session["cvm"] as CustomerViewModel;
            ProductDal pdal = new ProductDal();
            cvm.products = pdal.Products.ToList<Product>();
            return View(cvm);
        }
    }
}