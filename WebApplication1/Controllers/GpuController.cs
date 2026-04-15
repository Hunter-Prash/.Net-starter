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

        //get all gpus
        [HttpGet]
        public IActionResult GetAllGpus()
        {
            return Ok(gpus);
        }

        //get gpu by id
        [HttpGet("{id}")]
        public IActionResult GetGpu(int id)
        {
            var gpu = gpus.FirstOrDefault(x => x.Id == id);
            if (gpu == null)
            {
                return NotFound();
            }
            return Ok(gpu);
        }

        //add new gpu
        [HttpPost]
        public IActionResult AddGpu(Gpu gpu)
        {
            gpu.Id = gpus.Max(x => x.Id) + 1;
            gpus.Add(gpu);
            return Ok(gpu);

        }

        // DELETE GPU
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gpu = gpus.FirstOrDefault(x => x.Id == id);

            if (gpu == null)
                return NotFound();

            gpus.Remove(gpu);
            return Ok();
        }


    }
}
