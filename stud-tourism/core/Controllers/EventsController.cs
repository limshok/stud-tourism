using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : Controller
{
    private readonly MainContext _context;

    public EventsController(MainContext context)
    {
        _context = context;
    }

    // GET: api/Events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventModel>>> GetAllEvents()
    {
        return await _context.Events
            .Include(p => p.Images)
            .Include(p => p.University)
            .AsSplitQuery()
            .ToListAsync();
    }

    // GET: api/Events/2
    [HttpGet("{id}")]
    public async Task<ActionResult<EventModel>> GetEventItem(long id)
    {
        var eventItem = await _context.Events
            .Include(p => p.Images)
            .Include(p => p.University)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (eventItem == default)
            return NotFound();

        return eventItem;
    }

    // CREATE
    // POST: api/Events
    [HttpPost]
    public async Task<ActionResult<EventModel>> PostEventItem(EventModel eventModel)
    {
        _context.Events.Add(eventModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEventItem), new { id = eventModel.Id }, eventModel);
    }

    // UPDATE
    // POST: api/Events/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEventItem(long id, EventModel eventItem)
    {
        if (id != eventItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(eventItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!EventItemExist(id))
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

    // DELETE: api/Events/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEventItem(long id)
    {
        var eventItem = await _context.Events.FindAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }

        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventItemExist(long id)
    {
        return _context.Events.Any(e => e.Id == id);
    }
}