using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsRESTAPI.Models;

namespace RocketElevatorsFoundationFoundationRESTAPI.Controllers
{
    [Route("api/interventions")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly TodoContext _context;

        public InterventionsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            return await _context.interventions.ToListAsync();
        }

        // GET: api/interventions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Intervention>> GetIntervention(int id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }
        //Searches and returns Interventions with "Pending" status and with no start date
        ///Note: Case Insensitive
        ////GET api/interventions/Pending 
        [HttpGet("{status}")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetPendingInterventions (string status)
        {
            //Changes the received parameter {status} to lower cases and then checks if the status is "pending"
            //else it returns error 404
            status = status.ToLower();
            if (status == "pending")
            {
                //Search for all Interventions which have a "Pending" status and don't have a start date
                List<Intervention> interventionsList = (await _context.interventions.ToListAsync()).Where(interventions => interventions.status == "Pending" && interventions.start_date == null).ToList();
                //Returns error 404 if no Interventions meeting the previous line's criteria are found
                if (interventionsList == null)
                {
                    return NotFound();
                }
                else //Returns the found Interventions meeting the previous criteria
                {
                    return interventionsList; 
                }
            }
            else //Returns error 404 if {status} isn't "pending"
            {
                return NotFound();
            }
        }

        // PUT: api/interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(int id, Intervention intervention)
        {
            if (id != intervention.id)
            {
                return BadRequest();
            }

            _context.Entry(intervention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Changes selected Intervention's status and DateTime with parameter {status} and with today's date and time
        ///Note : Case Insensitive
        ////PUT api/interventions/id/InProgress or /id/Completed /id/Any-other-status
        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> PutInterventionStatus(int id, Intervention intervention, string status)
        {
            //Retrieves the selected Intervention and lowers the case of {status}
            var interventionFound = await _context.interventions.FindAsync(id);
            string statusToDown = status.ToLower();
            char[] lowered = statusToDown.ToCharArray();
            lowered[0] = char.ToUpper(lowered[0]);
            status = new string(lowered);
            //Checks if the lowered {status} is "inprogress" or "completed" to change the selected Interventions
            //status to it and to change the start date or the end date according to the verification
            if (statusToDown == "inprogress")                       
            {
                //Upper the {status} to make it comply with the desired format
                char[] loweredProg = status.ToCharArray();
                loweredProg[2] = char.ToUpper(loweredProg[2]);
                status = new string(loweredProg);

                interventionFound.status = status;
                interventionFound.start_date = DateTime.Now;
            }
            else if (statusToDown == "completed")
            {
                interventionFound.status = status;
                interventionFound.end_date = DateTime.Now;
            }
            else
            {
                interventionFound.status = status;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/interventions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntervention", new { id = intervention.id }, intervention);
        }

        // DELETE: api/interventions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntervention(int id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            _context.interventions.Remove(intervention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterventionExists(int id)
        {
            return _context.interventions.Any(e => e.id == id);
        }
    }
}