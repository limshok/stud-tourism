using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LodgingsController : Controller
{
    private readonly MainContext _context;

    public LodgingsController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Lodgings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LodgingModel>>> GetAllLodgings()
    {
        return await _context.Lodgings
            .Include(p => p.Images)
            .Include(p => p.University)
            .Include(p => p.Contact)
            .Include(p => p.Documents)
            .Include(p => p.Services)
            .Include(p => p.Rooms)
            .AsSplitQuery()
            .ToListAsync();
    }
    
    // GET: api/Lodgings/2
    [HttpGet("{id}")]
    public async Task<ActionResult<LodgingModel>> GetLodgingItem(long id)
    {
        var lodgingItem = await _context.Lodgings
            .Include(p => p.Images)
            .Include(p => p.University)
            .Include(p => p.Contact)
            .Include(p => p.Documents)
            .Include(p => p.Services)
            .Include(p => p.Rooms)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (lodgingItem == default)
            return NotFound();

        return lodgingItem;
    }
    
    // CREATE
    // POST: api/Lodgings
    [HttpPost]
    public async Task<ActionResult<LodgingModel>> PostLodgingItem(LodgingModel lodgingModel)
    {
        _context.Lodgings.Add(lodgingModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLodgingItem), new { id = lodgingModel.Id }, lodgingModel);
    }

    // UPDATE
    // POST: api/Lodgings/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLodgingItem(long id, LodgingModel lodgingItem)
    {
        if (id != lodgingItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(lodgingItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!LodgingItemExist(id))
            {
                return NotFound();
            }
            else
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        return NoContent();
    }
    
    // DELETE: api/Lodgings/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLodgingItem(long id)
    {
        var lodgingItem = await _context.Lodgings.FindAsync(id);
        if (lodgingItem == null)
        {
            return NotFound();
        }

        _context.Lodgings.Remove(lodgingItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool LodgingItemExist(long id)
    {
        return _context.Lodgings.Any(e => e.Id == id);
    }
}