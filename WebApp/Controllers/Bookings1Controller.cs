using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseModel;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class Bookings1Controller : Controller
    {
        private dat154_19_2Entities db = new dat154_19_2Entities();

        // GET: Bookings1
        public async Task<ActionResult> Index()
        {
            string user = "";
            if (Session["Username"] != null)
            {
                user = (string)Session["Username"];
            } else
            {
                return Redirect("~/");
            }
            var booking = db.Booking.Include(b => b.Customer).Include(b => b.Room).Where(b => b.CustomerUsername.Equals(user));
            return View(await booking.ToListAsync());
        }

        // GET: Bookings1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Booking.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings1/Create
        public ActionResult Create()
        {
            ViewBag.CustomerUsername = new SelectList(db.Customer, "Username", "Name");
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "RoomId");
            return View();
        }

        // GET: Bookings1/Create
        public ActionResult CreateFromRequest()
        {
            ViewBag.CustomerUsername = new SelectList(db.Customer, "Username", "Name");
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "RoomId");
            return View();
        }

        // POST: Bookings1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookingId,CustomerUsername,RoomId,CheckInDate,CheckOutDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Booking.Add(booking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerUsername = new SelectList(db.Customer, "Username", "Name", booking.CustomerUsername);
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }

        /// Lag en booking uten romnummer! -t
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateFromReservation([Bind(Include = "Username,NumberOfBeds,CheckInDate,CheckOutDate")] ReservationRequest reservation)
        {
            reservation.Username = (string) Session["Username"];
            Booking booking = RequestHandler.HandleRequest(reservation);

            if (ModelState.IsValid)
            {
                db.Booking.Add(booking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerUsername = new SelectList(db.Customer, "Username", "Name", booking.CustomerUsername);
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }



        // GET: Bookings1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Booking.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerUsername = new SelectList(db.Customer, "Username", "Name", booking.CustomerUsername);
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookingId,CustomerUsername,RoomId,CheckInDate,CheckOutDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerUsername = new SelectList(db.Customer, "Username", "Name", booking.CustomerUsername);
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Booking.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Booking booking = await db.Booking.FindAsync(id);
            db.Booking.Remove(booking);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
