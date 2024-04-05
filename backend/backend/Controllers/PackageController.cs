using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;

[Route("api/[controller]")]
[ApiController]
public class PackageController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PackageController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Package
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
    {
        return await _context.Package.ToListAsync();
    }

    // GET: api/Package/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Package>> GetPackage(int id)
    {
        var package = await _context.Package.FindAsync(id);

        if (package == null)
        {
            return NotFound();
        }

        return package;
    }
    // POST: api/Package
    [HttpPost]
    public async Task<ActionResult<Package>> PostPackage(Package package)
    {
        _context.Package.Add(package);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPackage), new { id = package.PackageID }, package);
    }
}