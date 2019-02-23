using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DatabaseModel;

namespace WebApp.Controllers
{
    public class RoomsController : ApiController
    {
        private dat154_19_2Entities db = new dat154_19_2Entities();

        // GET: api/Rooms
        public IQueryable<Room> GetRoom()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Room;
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> GetRoom(int id)
        {
            Room room = await db.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.RoomId)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Room.Add(room);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = room.RoomId }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> DeleteRoom(int id)
        {
            Room room = await db.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Room.Remove(room);
            await db.SaveChangesAsync();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Room.Count(e => e.RoomId == id) > 0;
        }
    }
}