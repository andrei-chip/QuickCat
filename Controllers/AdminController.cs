using QuickCat.Dal;
using QuickCat.Models;
using QuickCat.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuickCat.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminIndex(User usr)
        {
            CustomerDal Cdal = new CustomerDal();
            List<Customer> customers =(from u in Cdal.Customers where (u.Email==usr.Email) && (u.Password==usr.Password)
                    select u).ToList<Customer>();
            AdminViewModel model = new AdminViewModel();
            model.cvm.customer = customers[0];
            return View(model);
        }
        public ActionResult GetProductsByJson()
        {
            CustomerViewModel cvm = Session["cvm"] as CustomerViewModel;
            ProductDal pdal = new ProductDal();
            Thread.Sleep(3000);
            cvm.products= pdal.Products.ToList<Product>();
            return Json(cvm.products, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManageProducts()
        {
            CustomerViewModel cvm = Session["cvm"] as CustomerViewModel;
            ProductDal pdal = new ProductDal();
            cvm.products = pdal.Products.ToList<Product>();

            return View(cvm);
        }
        public ActionResult EditProducts()
        {
            CustomerViewModel cvm = Session["cvm"] as CustomerViewModel;
            ProductDal pdal = new ProductDal();
            cvm.products = pdal.Products.ToList<Product>();

            return View(cvm);
        }
        public ActionResult AddProducts()
        {
            CustomerViewModel cvm = Session["cvm"] as CustomerViewModel;
            ProductDal pdal = new ProductDal();
            cvm.products = pdal.Products.ToList<Product>();
            return View(cvm);
        }
        public ActionResult SearchProductsByField()
        {
            ProductDal pdal = new ProductDal();
            CustomerViewModel cvm = (Session["cvm"] as CustomerViewModel);
            string pName = Request.Form["txtProductName"].ToString();
            string pField = Request.Form["ProductField"].ToString();
            if (pField == "Name")
                cvm.products = (from x in pdal.Products
                                where x.ProductName.Contains(pName)
                                select x).ToList<Product>();
            else if (pField == "Price")
            {
                double price = Convert.ToDouble(pName);
                cvm.products = (from x in pdal.Products
                                where x.ProductPrice == price
                                select x).ToList<Product>();
            }
            else if (pField == "Category")
                cvm.products = (from x in pdal.Products
                                where x.ProductCategory.Contains(pName)
                                select x).ToList<Product>();
            else if (pField == "PetType")
                cvm.products = (from x in pdal.Products
                                where x.PetType.Contains(pName)
                                select x).ToList<Product>();
            else if (pField == "Description")
                cvm.products = (from x in pdal.Products
                                where x.ProductText.Contains(pName)
                                select x).ToList<Product>();
            return View("EditProducts", cvm);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SetAuthCookie("cookie", false);
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToRoute("Index");
        }
        [HttpPost]
        public ActionResult NewProduct()
        {
            Product newProduct = new Product();
            Thread.Sleep(3000);
            newProduct.ProductName = Request.Form["product.ProductName"].ToString();
            newProduct.PetType = Request.Form["product.PetType"].ToString();
            newProduct.ProductCategory = Request.Form["Category"].ToString();
            newProduct.ProductPrice = Convert.ToDouble(Request.Form["product.ProductPrice"].ToString());
            newProduct.ProductText = Request.Form["product.ProductText"].ToString();
            newProduct.ProductImg = Request.Form["product.ProductImg"].ToString();
            ProductDal pdal = new ProductDal();
            if (ModelState.IsValid)
            {
                pdal.Products.Add(newProduct);
                pdal.SaveChanges();
            }
            List<Product> products = pdal.Products.ToList<Product>();
            (Session["cvm"] as CustomerViewModel).products = products;
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveProduct()
        {
            int id = Convert.ToInt32(Request.Form.ToString());
            ProductDal pdal = new ProductDal();
            List<Product> products = (from x in pdal.Products
                            where x.ProductID==id
                            select x).ToList<Product>();
            pdal.Products.Remove(products[0]);
            pdal.SaveChanges();
            CustomerViewModel cvm = Session["cvm"] as CustomerViewModel;
            cvm.products = pdal.Products.ToList<Product>();
            return View("EditProducts", cvm);
        }
    }
}