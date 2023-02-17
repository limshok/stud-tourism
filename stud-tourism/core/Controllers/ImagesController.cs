using core.Data;
using core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : Controller
{
    private readonly MainContext _context;

    public ImagesController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Images
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ImageModel>>> GetAllImages()
    {
        return await _context.Images.ToListAsync();
    }
    
    // GET: api/Images/2
    [HttpGet("{id}")]
    public async Task<ActionResult<ImageModel>> GetImageItem(int id)
    {
        var imageItem = await _context.Images.FindAsync(id);
        
        if (imageItem == null)
            return NotFound();

        return imageItem;
    }
    
    // CREATE
    // POST: api/Images
    [HttpPost]
    public async Task<ActionResult<ImageModel>> PostImageItem(ImageModel imageModel)
    {
        _context.Images.Add(imageModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetImageItem), new { id = imageModel.Id }, imageModel);
    }

    // UPDATE
    // POST: api/Images/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutImageItem(int id, ImageModel imageItem)
    {
        if (id != imageItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(imageItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!ImageItemExist(id))
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
    
    // DELETE: api/Images/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImageItem(int id)
    {
        var imageItem = await _context.Images.FindAsync(id);
        if (imageItem == null)
        {
            return NotFound();
        }

        _context.Images.Remove(imageItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ImageItemExist(int id)
    {
        return _context.Images.Any(e => e.Id == id);
    }
}