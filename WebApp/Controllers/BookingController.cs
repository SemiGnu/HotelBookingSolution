using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using DatabaseModel;

namespace WebApp.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
       
        private dat154_19_2Entities db = new dat154_19_2Entities();

            public ActionResult Booking(Booking booking)
            {
                dynamic model = new ExpandoObject();
                if (booking == null)
                {
                    return View();
                }
                else if (!BookingPossible(booking))
                {
                    ViewBag.Message = "No available rooms in selected timespan";
                    return View(); 
                }
                else
                {
                    db.Booking.Add(booking); 
                    ViewBag.Message = "Your booking has been completed";
                    return View("Success"); 
                }
                
            }

            public bool BookingPossible(Booking booking)
            {
                return true; 
            }
    }
 
}