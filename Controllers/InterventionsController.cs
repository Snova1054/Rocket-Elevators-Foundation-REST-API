using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsRESTAPI.Models;

namespace RocketElevatorsRESTAPI.Controllers
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
        

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            return await _context.interventions.ToListAsync();
        }
        

        // GET: api/Interventions/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Intervention>> GetInterventions(int id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetPendingInterventions ()
        {
            List<Intervention> interventionsList = await _context.interventions.ToListAsync();
            List<Intervention> filteredList = new List<Intervention>(); 
            filteredList = interventionsList.Where(intervention => intervention.status == "Pending" && intervention.start == null).ToList();
            if (filteredList == null)
            {
                return NotFound();
            }
            else
            {
                return filteredList; 
            }
        }

        [HttpPut("{id}/InterventionStart")]
        public async Task<IActionResult> startingIntervention(int id, Intervention intervention)
        {
            var InterventionFound = await _context.interventions.FindAsync(id);

            InterventionFound.status = "InProgress";
            InterventionFound.start = DateTime.Now.ToString();
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

        [HttpPut("{id}/InterventionEnd")]
        public async Task<IActionResult> endingIntervention(int id, Intervention intervention)
        {
            var InterventionFound = await _context.interventions.FindAsync(id);

            InterventionFound.status = "Completed";
            InterventionFound.end = DateTime.Now.ToString();
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

        private bool InterventionExists(int id)
        {
            return _context.interventions.Any(e => e.id == id);
        }
    }
}