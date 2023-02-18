using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly MainContext _context;

    public ContactsController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Contacts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactModel>>> GetAllContacts()
    {
        return await _context.Contacts.ToListAsync();
    }
    
    // GET: api/Contacts/2
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactModel>> GetContactItem(long id)
    {
        var contactItem = await _context.Contacts.FindAsync(id);
        
        if (contactItem == null)
            return NotFound();

        return contactItem;
    }
    
    // CREATE
    // POST: api/Contacts
    [HttpPost]
    public async Task<ActionResult<ContactModel>> PostContactItem(ContactModel contactModel)
    {
        _context.Contacts.Add(contactModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContactItem), new { id = contactModel.Id }, contactModel);
    }

    // UPDATE
    // POST: api/Contacts/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutContactItem(long id, ContactModel contactItem)
    {
        if (id != contactItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(contactItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!ContactItemExist(id))
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

    // DELETE: api/Contacts/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactItem(long id)
    {
        var contactItem = await _context.Contacts.FindAsync(id);
        if (contactItem == null)
        {
            return NotFound();
        }

        _context.Contacts.Remove(contactItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContactItemExist(long id)
    {
        return _context.Contacts.Any(e => e.Id == id);
    }
}