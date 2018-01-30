using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickCat.Models;
using QuickCat.Dal;
using System.Web.Security;
using QuickCat.ModelView;


namespace QuickCat.Controllers
{
    public class UserController : Controller
    {
        //redirect to Index
        public ActionResult Index()
        {
            return RedirectToRoute("Index");
        }
        // GET: User
        public ActionResult Login()
        {
            CustomerViewModel cvm = new CustomerViewModel();
            cvm.customer = new Customer();
            return View(cvm);
        }

        public ActionResult Authenticate(CustomerViewModel cvm)
        {
            CustomerDal dal = new CustomerDal();
            if(ModelState.IsValid)
            {
                List<Customer> userValidated = (from u in dal.Customers
                                            where (u.Email == cvm.user.Email) && (u.Password == cvm.user.Password)
                                            select u).ToList<Customer>();
                

                if (userValidated.Count != 0)
                {
                    cvm.customer = userValidated[0];
                    FormsAuthentication.SetAuthCookie("cookie", true);
                    Session["cvm"] = cvm;
                    return RedirectToAction( "Index2","Home",cvm);
                }
                else
                {
                    cvm.ErrorMessage = "Check your enter please";
                    return View("Login", cvm);
                }
            }
            return View("Login",cvm);
        }
        public ActionResult Register(CustomerViewModel model)
        {
            CustomerViewModel cvm = new CustomerViewModel();
            cvm.customer = new Customer();
            CustomerDal dal = new CustomerDal();
            List<Customer> objCustomers = dal.Customers.ToList<Customer>();
            cvm.customers = objCustomers;

            return View(cvm);
        }
        [HttpPost]
        public ActionResult Submit()
        {
            CustomerViewModel cvm = new CustomerViewModel();
            Customer objCustomer = new Models.Customer();
            objCustomer.ID = Request.Form["customer.ID"].ToString();
            objCustomer.FirstName = Request.Form["customer.FirstName"].ToString();
            objCustomer.LastName = Request.Form["customer.LastName"].ToString();
            objCustomer.City = Request.Form["customer.City"].ToString();
            objCustomer.Address = Request.Form["customer.Address"].ToString();
            objCustomer.Email = Request.Form["customer.Email"].ToString();
            objCustomer.Password = Request.Form["customer.Password"].ToString();
            objCustomer.Permission = 0;
            cvm.customer = objCustomer;
            CustomerDal dal = new CustomerDal();
            List<Customer> objCustomers = dal.Customers.ToList<Customer>();
            cvm.customers = objCustomers;
            if (ModelState.IsValid)
            {

                if ((from u in dal.Customers where u.ID == objCustomer.ID select u).Count() != 0)
                {
                    ModelState.AddModelError("customer.ID", "This ID already register!");
                    return View("Register", cvm);
                }
                else
                {
                    if ((from u in dal.Customers where u.Email == objCustomer.Email select u).Count() != 0)
                    {
                        ModelState.AddModelError("customer.Email", "This email already register!");
                        return View("Register", cvm);
                    }
                    dal.Customers.Add(objCustomer);
                    dal.SaveChanges();
                    FormsAuthentication.SetAuthCookie("cookie", true);
                    Session["cvm"] = cvm;
                    return RedirectToAction("Index2", "Home", cvm);
                    //return View("Register", cvm);
                }
            }
            return View("Register", cvm);
        }
        public ActionResult AddProductToCart()
        {
            int id = Convert.ToInt32(Request.Form.ToString());
            ProductDal pdal = new ProductDal();
            List<Product> products = (from x in pdal.Products
                                      where x.ProductID == id
                                      select x).ToList<Product>();
            if ((Session["cvm"] as CustomerViewModel).cart == null)
                (Session["cvm"] as CustomerViewModel).cart = new List<Product>();
            (Session["cvm"] as CustomerViewModel).cart.Add(products[0]);
            Session["TotalPrice"]= Convert.ToDouble(Session["TotalPrice"]) + products[0].ProductPrice; 
            return View(Session["cvm"] as CustomerViewModel);
        }
        public ActionResult Cart()
        {
            return View(Session["cvm"] as CustomerViewModel);
        }
        public ActionResult RemoveFromCart()
        {
            int id = Convert.ToInt32(Request.Form.ToString());
            List<Product> products = (Session["cvm"] as CustomerViewModel).cart;
            foreach (Product x in (Session["cvm"] as CustomerViewModel).cart)
            {
                if (x.ProductID == id)
                {
                    (Session["cvm"] as CustomerViewModel).cart.Remove(x);
                    Session["TotalPrice"] = Convert.ToDouble(Session["TotalPrice"]) - x.ProductPrice;
                    return View("Cart", Session["cvm"] as CustomerViewModel);
                }
            }
            return View("Cart",Session["cvm"] as CustomerViewModel);
        }
        public ActionResult CheckOut()
        {
            
            CustomerViewModel cvm = Session["cvm"] as CustomerViewModel;
            OrderDal orders = new OrderDal();
            //List<Order> orderss = orders.Orders.ToList<Order>();
            Order ord = new Order();
            ord.Date = DateTime.Today;
            ord.Email = cvm.customer.Email;
            int max = orders.Orders.Max(p => p.OrderID);
            ord.OrderID = max+1;
            foreach (Product prod in cvm.cart)
            {
                ord.ProductID = prod.ProductID;
                //ord.OrderID = 1;
                orders.Orders.Add(ord);
                orders.SaveChanges();
            }
            (Session["cvm"] as CustomerViewModel).cart = null;
            Session["TotalPrice"] = 0;

            return View("Cart", Session["cvm"] as CustomerViewModel);
        }
    }
}