using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DatabaseModel;
using System.Web.Http;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private dat154_19_2Entities db = new dat154_19_2Entities();
        private string username;
        private string password;
        private string fullName; 

        public void SaveData(FormCollection fomr)
        {
            username = Request.Form["name"];
            password = Request.Form["surname"]; 

        }

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
        public ActionResult LogInAction()
        {
            FormCollection collect = new FormCollection();
            username = Request.Form["usernameLogin"];
            password = Request.Form["passwordLogin"]; 
            //username = collect.GetValue().ToString();
            //password = collect.GetValue(password).ToString(); 
            Console.WriteLine(username, password);
            ViewBag.Message = username + password;
            return View(); 
            //username = Request.Form[FormCollection.username]; 

            Customer customer;
            
            //dac.Customer = this.customer; 
         
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostCusomerTask([Bind(Include = "Username,Name,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");


            }
            

            return View();

          


        }


    }
    
}