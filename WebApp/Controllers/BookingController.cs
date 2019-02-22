using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DesktopApp;


namespace WebApp.Controllers
{
    public class BookingController : Controller
    {
        
        DesktopAppConfig dac = new DesktopAppConfig();
        DbSet<Room> room;
        DbSet<Booking> booking;
        DbSet<Customer> customer;


        public bool addUser()
        {
            Customer customer;
            string username = Request.Form[usernameLogin];
            //dac.Customer = this.customer; 
            return true;
        }

        public bool CheckUserNameExists(string username)
        {
            room = dac.Room;
            booking = dac.Booking;
            customer = dac.Customer;

            room.Load();
            booking.Load();
            customer.Load();


            return true; 
        }
    }
}