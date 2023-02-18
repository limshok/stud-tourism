using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RandomizeController : Controller
{
    private readonly MainContext _context;

    public RandomizeController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Randomize
    [HttpGet]
    public async Task<ActionResult<LodgingModel>> GetAllLodgings()
    {
        var lodgings = await _context.Lodgings
            .Include(p => p.Images)
            .Include(p => p.University)
            .Include(p => p.Contact)
            .Include(p => p.Documents)
            .Include(p => p.Services)
            .Include(p => p.Rooms)
            .AsSplitQuery()
            .ToListAsync();

        Random rnd = new Random();
        int rndIndex = rnd.Next(lodgings.Count);

        return lodgings[rndIndex];
    }
}