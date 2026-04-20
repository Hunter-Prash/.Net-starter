using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/test")]//BASE ROUTE


    public class Test : ControllerBase //ControllerBase is a base class for an MVC controller without view support. It provides properties and methods that are useful for handling HTTP requests and generating HTTP responses in a Web API application.Also it is used to create RESTful services that can be consumed by clients such as web browsers, mobile apps, or other servers. By inheriting from ControllerBase, you can easily create API endpoints that return data in formats like JSON or XML, and you can use attributes like [HttpGet], [HttpPost], etc., to define the HTTP methods for your actions.
    {
        private readonly AppDbContext _context;

        public Test(AppDbContext context)//constructor injection - dependency injection (DI) is a design pattern used to implement IoC, allowing for better modularity and easier testing. In this case, the AppDbContext is injected into the Test controller, enabling it to interact with the database without having to create an instance of AppDbContext directly within the controller.
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var data = await _context.Incidents.ToListAsync();

            /*
             EF me 2 tareeke hain join karne ke:
            1. Navigation (Include) - EF automatically creates a join based on the relationships defined in your model(C# object). You can use the Include(left join) method to specify related entities to include in the query results. This is a more convenient and efficient way to retrieve related data.

            2. Manual (Join / Select)- You can write a manual join using LINQ to query the database and retrieve related data. This approach gives you more control over the query but can be more complex and less efficient than using navigation properties.
            */


            //join using navigation properties (Include) typically left join . If inner join is needed then just write join instead of include and specify the join condition.
            var data = await _context.Incidents
            .Include(i => i.User)
             .ToListAsync();
            return Ok(data);

            //filter
            var data=await _context.Incidents.Where(i=>i.State=="open").ToListAsync();

            //inner join using manual join.basically used when we dont have navigation properties defined in our model or when we want to perform more complex joins that are not easily expressed using navigation properties. 
            //.Join requires 4 params(innerSequence, outerKeySelector, innerKeySelector, resultSelector)
            var data = await _context.Incidents
        .Join(
            _context.Users,        // second table to join
            i => i.UserId,         // key from Incidents
            u => u.Id,             // key from Users
            (i, u) => new {        // result selector (required 4th param)
                i.Title,
                i.Description,
                i.State,
                i.CreatedAt,
                UserName = u.Name  // u comes from Users, not i
            }
        )
        .OrderByDescending(x => x.CreatedAt)
        .ToListAsync();

        }


        //so the final route will be api/test/{id}.THis is called route parameter.Itss diffrent from query parameters which are passed after a question mark in the URL (e.g., api/test?id=123). Route parameters are part of the URL path and are typically used to identify specific resources, while query parameters are used to filter or modify the response without changing the resource being accessed.Example of query parameter: api/test?state=open.
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
