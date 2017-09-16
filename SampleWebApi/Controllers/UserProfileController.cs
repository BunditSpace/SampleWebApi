using SampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SampleWebApi.Controllers
{
    public class UserProfileController : ApiController
    {
        private SampleDbContext db = new SampleDbContext();

        // GET: api/UserProfile  
        public IQueryable<UserProfile> GetUserProfile()
        {
            return db.UserProfile;
        }

        // GET: api/UserProfile/5  
        [ResponseType(typeof(UserProfile))]
        public async Task<IHttpActionResult> GetUserProfile(int id)
        {
            UserProfile userProfile = await db.UserProfile.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        // PUT: api/UserProfile/5  
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserProfile(int id, UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProfile.id)
            {
                return BadRequest();
            }

            db.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollegeDetailExists(id))
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

        // POST: api/UserProfile  
        [ResponseType(typeof(UserProfile))]
        public async Task<IHttpActionResult> PostCollegeDetail(UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProfile.Add(userProfile);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userProfile.id }, userProfile);
        }

        // DELETE: api/UserProfile/5  
        [ResponseType(typeof(UserProfile))]
        public async Task<IHttpActionResult> DeleteUserProfile(int id)
        {
            UserProfile userProfile = await db.UserProfile.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            db.UserProfile.Remove(userProfile);
            await db.SaveChangesAsync();

            return Ok(userProfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CollegeDetailExists(int id)
        {
            return db.UserProfile.Count(e => e.id == id) > 0;
        }

    }
}
