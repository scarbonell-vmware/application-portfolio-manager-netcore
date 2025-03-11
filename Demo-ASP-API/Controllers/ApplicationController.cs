namespace Demo_ASP_API.Controllers
{
    using Demo_ASP_API.Data;
    using Demo_ASP_API.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;



    [Route("/api/v1/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApplicationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost ("seed")]
        public async Task<IActionResult> CreateSeeds()
        {
            List<Models.Application> apps = await _context.Applications.ToListAsync();
            if (apps != null && apps.Count == 0)
            {
                Application defaultApp = new Application();
                defaultApp.setName("Finance 2.0");
                defaultApp.setDescription("Manage Stock Trades");
                defaultApp.setBusinessEntity("CDE");
                defaultApp.setOrganization("FIN");
                defaultApp.setBusinessOwner("Martin Bacle");
                defaultApp.setIdentifier("Fin2-0");

                 
                 _context.Applications.Add(defaultApp);
                await _context.SaveChangesAsync();
                //apps = await _context.Applications.ToListAsync();
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var article = await _context.Applications.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
        
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id,Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var app = await _context.Applications.FindAsync(id);

            if (app == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(app);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
