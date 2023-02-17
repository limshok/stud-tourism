using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LodgingRoomsController : Controller
{
    private readonly MainContext _context;

    public LodgingRoomsController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/LodgingRooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LodgingRoomModel>>> GetAllLodgingRooms()
    {
        return await _context.LodgingRooms.ToListAsync();
    }
    
    // GET: api/LodgingRooms/2
    [HttpGet("{id}")]
    public async Task<ActionResult<LodgingRoomModel>> GetLodgingRoomItem(int id)
    {
        var lodgingRoomItem = await _context.LodgingRooms.FindAsync(id);
        
        if (lodgingRoomItem == null)
            return NotFound();

        return lodgingRoomItem;
    }
    
    // CREATE
    // POST: api/LodgingRooms
    [HttpPost]
    public async Task<ActionResult<LodgingRoomModel>> PostLodgingRoomItem(LodgingRoomModel lodgingRoomModel)
    {
        _context.LodgingRooms.Add(lodgingRoomModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLodgingRoomItem), new { id = lodgingRoomModel.Id }, lodgingRoomModel);
    }

    // UPDATE
    // POST: api/LodgingRooms/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLodgingRoomItem(int id, LodgingRoomModel lodgingRoomItem)
    {
        if (id != lodgingRoomItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(lodgingRoomItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!LodgingRoomItemExist(id))
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
    
    // DELETE: api/LodgingRooms/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLodgingRoomItem(int id)
    {
        var lodgingRoomItem = await _context.LodgingRooms.FindAsync(id);
        if (lodgingRoomItem == null)
        {
            return NotFound();
        }

        _context.LodgingRooms.Remove(lodgingRoomItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool LodgingRoomItemExist(int id)
    {
        return _context.LodgingRooms.Any(e => e.Id == id);
    }
}