using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoryRecommendationController : Controller
{
    private readonly MainContext _context;

    public StoryRecommendationController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/StoryRecommendation
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LodgingModel>>> GetFilterLodgings()
    {
        string regionFilter = "Алтайский край";

        return await _context.Lodgings
            .Include(p => p.Images)
            .Include(p => p.University)
            .Include(p => p.Contact)
            .Include(p => p.Documents)
            .Include(p => p.Services)
            .Include(p => p.Rooms)
            .AsSplitQuery()
            .Where(p => p.University.District.Contains(regionFilter))
            .ToListAsync();;
    }
}