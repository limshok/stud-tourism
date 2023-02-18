using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UniversitiesController : Controller
{
    private readonly MainContext _context;

    public UniversitiesController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Universities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UniversityModel>>> GetAllUniversities()
    {
        return await _context.Universities.ToListAsync();
    }
    
    // GET: api/Universities/2
    [HttpGet("{id}")]
    public async Task<ActionResult<UniversityModel>> GetUniversityItem(long id)
    {
        var universityItem = await _context.Universities.FindAsync(id);
        
        if (universityItem == null)
            return NotFound();

        return universityItem;
    }
    
    // CREATE
    // POST: api/Universities
    [HttpPost]
    public async Task<ActionResult<UniversityModel>> PostUniversityItem(UniversityModel universityModel)
    {
        _context.Universities.Add(universityModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUniversityItem), new { id = universityModel.Id }, universityModel);
    }

    // UPDATE
    // POST: api/Universities/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUniversityItem(long id, UniversityModel universityItem)
    {
        if (id != universityItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(universityItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!UniversityItemExist(id))
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
    
    // DELETE: api/Universities/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUniversityItem(long id)
    {
        var universityItem = await _context.Universities.FindAsync(id);
        if (universityItem == null)
        {
            return NotFound();
        }

        _context.Universities.Remove(universityItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool UniversityItemExist(long id)
    {
        return _context.Universities.Any(e => e.Id == id);
    }
}