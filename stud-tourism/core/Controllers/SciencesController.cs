using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SciencesController : Controller
{
    private readonly MainContext _context;

    public SciencesController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Sciences
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScienceModel>>> GetAllSciences()
    {
        return await _context.Sciences
            .Include(p => p.Images)
            .Include(p => p.University)
            .Include(p => p.ContactModel)
            .AsSplitQuery()
            .ToListAsync();
    }
    
    // GET: api/Sciences/2
    [HttpGet("{id}")]
    public async Task<ActionResult<ScienceModel>> GetScienceItem(long id)
    {
        var scienceItem = await _context.Sciences
            .Include(p => p.Images)
            .Include(p => p.University)
            .Include(p => p.ContactModel)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (scienceItem == null)
            return NotFound();

        return scienceItem;
    }
    
    // CREATE
    // POST: api/Sciences
    [HttpPost]
    public async Task<ActionResult<ScienceModel>> PostScienceItem(ScienceModel scienceModel)
    {
        _context.Sciences.Add(scienceModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetScienceItem), new { id = scienceModel.Id }, scienceModel);
    }

    // UPDATE
    // POST: api/Sciences/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutScienceItem(long id, ScienceModel scienceItem)
    {
        if (id != scienceItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(scienceItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!ScienceItemExist(id))
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
    
    // DELETE: api/Sciences/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScienceItem(long id)
    {
        var scienceItem = await _context.Sciences.FindAsync(id);
        if (scienceItem == null)
        {
            return NotFound();
        }

        _context.Sciences.Remove(scienceItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ScienceItemExist(long id)
    {
        return _context.Sciences.Any(e => e.Id == id);
    }
}