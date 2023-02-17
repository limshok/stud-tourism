using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : Controller
{
    private readonly MainContext _context;

    public ServicesController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Services
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceModel>>> GetAllServices()
    {
        return await _context.Services.ToListAsync();
    }
    
    // GET: api/Services/2
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceModel>> GetServiceItem(int id)
    {
        var serviceItem = await _context.Services.FindAsync(id);
        
        if (serviceItem == null)
            return NotFound();

        return serviceItem;
    }
    
    // CREATE
    // POST: api/Services
    [HttpPost]
    public async Task<ActionResult<ServiceModel>> PostServiceItem(ServiceModel serviceModel)
    {
        _context.Services.Add(serviceModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetServiceItem), new { id = serviceModel.Id }, serviceModel);
    }

    // UPDATE
    // POST: api/Services/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutServiceItem(int id, ServiceModel serviceItem)
    {
        if (id != serviceItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(serviceItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!ServiceItemExist(id))
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
    
    // DELETE: api/Services/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceItem(int id)
    {
        var serviceItem = await _context.Services.FindAsync(id);
        if (serviceItem == null)
        {
            return NotFound();
        }

        _context.Services.Remove(serviceItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ServiceItemExist(int id)
    {
        return _context.Services.Any(e => e.Id == id);
    }
}