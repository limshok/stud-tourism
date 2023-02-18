using core.Data;
using core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HashtagsController : Controller
{
    private readonly MainContext _context;

    public HashtagsController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Hashtags
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HashtagModel>>> GetAllHashtags()
    {
        return await _context.Hashtags.ToListAsync();
    }
    
    // GET: api/Hashtags/2
    [HttpGet("{id}")]
    public async Task<ActionResult<HashtagModel>> GetHashtagItem(long id)
    {
        var hashtagItem = await _context.Hashtags.FindAsync(id);
        
        if (hashtagItem == null)
            return NotFound();

        return hashtagItem;
    }
    
    // CREATE
    // POST: api/Hashtags
    [HttpPost]
    public async Task<ActionResult<HashtagModel>> PostHashtagItem(HashtagModel hashtagModel)
    {
        _context.Hashtags.Add(hashtagModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHashtagItem), new { id = hashtagModel.Id }, hashtagModel);
    }

    // UPDATE
    // POST: api/Hashtags/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHashtagItem(long id, HashtagModel hashtagItem)
    {
        if (id != hashtagItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(hashtagItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!HashtagItemExist(id))
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
    
    // DELETE: api/Hashtags/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHashtagItem(long id)
    {
        var hashtagItem = await _context.Hashtags.FindAsync(id);
        if (hashtagItem == null)
        {
            return NotFound();
        }

        _context.Hashtags.Remove(hashtagItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool HashtagItemExist(long id)
    {
        return _context.Hashtags.Any(e => e.Id == id);
    }
}