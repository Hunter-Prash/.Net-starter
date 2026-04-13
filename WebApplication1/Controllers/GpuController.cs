using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GpuController : ControllerBase
    {
        private static List<Gpu> gpus = new List<Gpu>
        {
            new Gpu { Id = 1, Brand = "NVIDIA", Name = "GeForce RTX 3080", MemorySize = 10, Price = 699 },
            new Gpu { Id = 2, Brand = "AMD", Name = "Radeon RX 6800 XT", MemorySize = 16, Price = 649 },
            new Gpu { Id = 3, Brand = "NVIDIA", Name = "GeForce RTX 3070", MemorySize = 8, Price = 499 }
        };

        [HttpGet]


    }
}
