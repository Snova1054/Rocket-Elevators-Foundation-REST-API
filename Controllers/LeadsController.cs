using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsRESTAPI.Models;

namespace RocketElevatorsRESTAPI.Controllers
{
    [Route("api/leads")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly TodoContext _context;

        public LeadsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeads()
        {
            return await _context.leads.ToListAsync();
        }

        // GET: api/Leads/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Lead>> GetLead(int id)
        {
            var lead = await _context.leads.FindAsync(id);

            if (lead == null)
            {
                return NotFound();
            }

            return lead;
        }
        [HttpGet("requestedInfo")]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeadsInfos(string requestedInfo)
        {
            DateTime monthAgo = DateTime.Now.AddDays(-30);
            List<Lead> leadsList = await _context.leads.ToListAsync();
            List<Lead> filteredLeadsList = leadsList.Where(lead => lead.created_at > monthAgo).ToList();
            List<Customer> customersList = await _context.customers.ToListAsync();
            List<Lead> leadsMadeCustomersList = new List<Lead>();
            
            for (int i = 0; i < filteredLeadsList.Count;)
            {
                for (int j = 0; j < customersList.Count; j++)
                {
                    if (filteredLeadsList[i].email == customersList[j].email_of_the_company_contact)
                    {
                        i++; j = -1;
                    }
                }
                leadsMadeCustomersList.Add(filteredLeadsList[i]); i++;
            }
            if (leadsMadeCustomersList == null)
            {
                return NotFound();                
            }
            else
            {
                return leadsMadeCustomersList;
            }
        }


        // PUT: api/Leads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLead(int id, Lead lead)
        {
            if (id != lead.id)
            {
                return BadRequest();
            }

            _context.Entry(lead).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadExists(id))
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

        // POST: api/Leads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lead>> PostLead(Lead lead)
        {
            _context.leads.Add(lead);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLead", new { id = lead.id }, lead);
        }

        // DELETE: api/Leads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead(int id)
        {
            var lead = await _context.leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            _context.leads.Remove(lead);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeadExists(int id)
        {
            return _context.leads.Any(e => e.id == id);
        }
    }
}
