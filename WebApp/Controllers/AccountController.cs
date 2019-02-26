using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        SessionContext context = new SessionContext();
        private dat154_19_2Entities db = new dat154_19_2Entities();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer user)
        {

            var authenticatedUser = db.Customer.Where(c => c.Username == user.Username && c.Password == c.Password).FirstOrDefault();
            if (authenticatedUser != null)
            {
                context.SetAuthenticationToken(authenticatedUser.Username.ToString(), false, authenticatedUser);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}