using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DesktopApp;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class TaskController : Controller
    {
        DesktopAppConfig dac = new DesktopAppConfig();
        DbSet<Room> room;
        DbSet<Booking> booking;
        DbSet<Customer> customer;
        DbSet<Task> task;
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }
    }
}