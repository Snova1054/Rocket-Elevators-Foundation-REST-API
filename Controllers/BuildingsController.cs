using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsRESTAPI.Models;

namespace RocketElevatorsRESTAPI.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly TodoContext _context;

        public BuildingsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        // GET: api/Buildings/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Building>> GetBuilding(int id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }
        [HttpGet("{status}")]
        public IEnumerable<Building> GetBuildingsIntervention(string status) 
        {
            IQueryable<Building> Building = from building in _context.buildings
                join battery in _context.batteries on building.id equals battery.building_id
                join column in _context.columns on battery.id equals column.battery_id
                join elevator in _context.elevators on column.id equals elevator.column_id
                where battery.status == status || column.status == status || elevator.status == status
                select building;
            List<Building> buildingIntervention = new List<Building>();
           
            buildingIntervention = (Building.ToList()).Distinct().ToList();
             
            return buildingIntervention;
        }

        [HttpGet("customer/{requestedID:int}")]
        public async Task<ActionResult<IEnumerable<Building>>> GetCustomerBuildings(int requestedID)
        {
            List<Building> buildingsList = (await _context.buildings.ToListAsync()).Where(building => building.customer_id == requestedID).ToList();

            if (buildingsList.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return buildingsList;
            }
        }


        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(int id, Building building)
        {
            if (id != building.id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(int id)
        {
            return _context.buildings.Any(e => e.id == id);
        }
    }
}
