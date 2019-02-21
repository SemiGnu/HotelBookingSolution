using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

 

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        bool loggedIn = false;



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Create new user";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Enter login info";

            return View();
        }
        public ActionResult Booking()
        {
            if (loggedIn)
            {
                return View(); 
            }
            else
            {
                ViewBag.Message = "Wrong name or password";
                return Login();
                
            }
        }
    }
}