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
                    var obj = db.Customer.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Username"] = obj.Username.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Login");
        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(Customer objUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (dat154_19_2Entities db = new dat154_19_2Entities())
        //        {
        //            var obj = db.Customer.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
        //            if (obj != null)
        //            {
        //                Session["UserName"] = obj.Username.ToString();
        //                return RedirectToAction("UserDashBoard");
        //            }
        //        }
        //    }
        //    return View(objUser);
        //}

        //public ActionResult UserDashBoard()
        //{
        //    if (Session["UserID"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

    }
}