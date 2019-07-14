using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTracker.Backservice.Data;
using TripTracker.Backservice.Models;

namespace TripTracker.Backservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        TripContext dbContext;

        public TripsController(TripContext tripContext)
        {
            dbContext = tripContext;
        }

        // GET api/trips
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<Trip> trips = await dbContext.Trips.ToListAsync();

            return Ok(trips);
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> Get(int id)
        {
            Trip trip = await dbContext.Trips.FindAsync(id);

            return Ok(trip);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Trip value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbContext.Trips.Add(value);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Trip value)
        {
            if (!dbContext.Trips.Any(t => t.Id == id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.Trips.Update(value);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trip = dbContext.Trips.Find(id);

            if(trip == null)
            {
                return NotFound();
            }

            dbContext.Trips.Remove(trip);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
