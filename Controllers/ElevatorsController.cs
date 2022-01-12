using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsRESTAPI.Models;

namespace RocketElevatorsRESTAPI.Controllers
{
    [Route("api/elevators")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly TodoContext _context;

        public ElevatorsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> Getelevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // GET: api/Elevators/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Elevator>> GetElevator(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetInterventionElevator (string status)
        {
            List<Elevator> elevatorsList = await _context.elevators.ToListAsync();
            List<Elevator> filteredList = new List<Elevator>(); 
            filteredList = elevatorsList.Where(elevator => elevator.status == status).ToList();

            if (filteredList == null)
            {
                return NotFound();
            }
            else
            {
                return filteredList; 
            }
        }

        [HttpGet("notoperational/all")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetInterventionElevator ()
        {
            List<Elevator> elevatorsList = await _context.elevators.ToListAsync();
            List<Elevator> filteredList = new List<Elevator>(); 
            filteredList = elevatorsList.Where(elevator => elevator.status == "intervention" || elevator.status == "offline").ToList();
            
            if (filteredList == null)
            {
                return NotFound();
            }
            else
            {
                return filteredList; 
            }
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevator(int id, Elevator elevator)
        {
            if (id != elevator.id)
            {
                return BadRequest();
            }

            _context.Entry(elevator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
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

        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> PutElevatorStatus(int id, Elevator elevator, string status)
        {
            var elevatorFound = await _context.elevators.FindAsync(id);

            elevatorFound.status = status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
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

        // POST: api/Elevators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        {
            _context.elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElevator", new { id = elevator.id }, elevator);
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevator(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElevatorExists(int id)
        {
            return _context.elevators.Any(e => e.id == id);
        }
    }
}
