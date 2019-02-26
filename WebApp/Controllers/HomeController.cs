using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseModel;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            return View("Index");
        }

        public ActionResult LogInForwardActionResult()
        {
            return Redirect("~/Bookings1/Index");
        }

        // GET
        public ActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }
    
        // POST
        public ActionResult LoginPost(Customer objUser)
        {
            if (ModelState.IsValid)
            {
                using (dat154_19_2Entities db = new dat154_19_2Entities())
                {
                    var obj = db.Customer.FirstOrDefault(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password));
                    if (obj != null)
                    {
                        Session["Username"] = obj.Username.ToString();
                        return RedirectToAction("LogInForwardActionResult");
                    }
                }
            }
            return RedirectToAction("Login");
        }




        

    }
}