using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/test")]//BASE ROUTE
    public class Test : ControllerBase
    {
        private readonly AppDbContext _context;

        public Test(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var data = await _context.Incidents.ToListAsync();

            /*
             EF me 2 tareeke hain join karne ke:
            1. Navigation (Include) - EF automatically creates a join based on the relationships defined in your model(C# object). You can use the Include method to specify related entities to include in the query results. This is a more convenient and efficient way to retrieve related data.

            2. Manual (Join / Select)- You can write a manual join using LINQ to query the database and retrieve related data. This approach gives you more control over the query but can be more complex and less efficient than using navigation properties.
            */

            var data = await _context.Incidents
            .Include(i => i.User)
             .ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }
            return Ok(incident);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Incident incident)
        {

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();
            return Ok(incident);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound();
            }

            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();
            return Ok("Deleted successfully");
        }
    }
}
