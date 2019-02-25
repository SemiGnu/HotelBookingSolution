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

        public ActionResult Register()
        {
            return View("~/Views/Customers/Register.cshtml");
        }

        public ActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }




    }
}