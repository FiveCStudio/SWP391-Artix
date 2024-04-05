using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentPackageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CurrentPackageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CurrentPackage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrentPackage>>> GetCurrentPackages()
        {
            return await _context.CurrentPackage.ToListAsync();
        }

        // GET: api/CurrentPackage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentPackage>> GetCurrentPackage(int id)
        {
            var currentPackage = await _context.CurrentPackage.FindAsync(id);

            if (currentPackage == null)
            {
                return NotFound();
            }

            return currentPackage;
        }




    }
}
