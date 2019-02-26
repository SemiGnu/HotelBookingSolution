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

namespace UwpDbConnect.Controllers
{
    public class ServiceTasksController : ApiController
    {
        private dat154_19_2Entities db = new dat154_19_2Entities();

        // GET: api/ServiceTasks
        public IQueryable<ServiceTask> GetServiceTask()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ServiceTask;
        }

        // GET: api/ServiceTasks/5
        [ResponseType(typeof(ServiceTask))]
        public async Task<IHttpActionResult> GetServiceTask(int id)
        {
            ServiceTask serviceTask = await db.ServiceTask.FindAsync(id);
            if (serviceTask == null)
            {
                return NotFound();
            }

            return Ok(serviceTask);
        }

        // PUT: api/ServiceTasks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutServiceTask(int id, ServiceTask serviceTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceTask.TaskId)
            {
                return BadRequest();
            }

            db.Entry(serviceTask).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceTaskExists(id))
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

        // POST: api/ServiceTasks
        [ResponseType(typeof(ServiceTask))]
        public async Task<IHttpActionResult> PostServiceTask(ServiceTask serviceTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceTask.Add(serviceTask);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = serviceTask.TaskId }, serviceTask);
        }

        // DELETE: api/ServiceTasks/5
        [ResponseType(typeof(ServiceTask))]
        public async Task<IHttpActionResult> DeleteServiceTask(int id)
        {
            ServiceTask serviceTask = await db.ServiceTask.FindAsync(id);
            if (serviceTask == null)
            {
                return NotFound();
            }

            db.ServiceTask.Remove(serviceTask);
            await db.SaveChangesAsync();

            return Ok(serviceTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceTaskExists(int id)
        {
            return db.ServiceTask.Count(e => e.TaskId == id) > 0;
        }
    }
}