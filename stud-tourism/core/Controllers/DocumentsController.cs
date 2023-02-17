using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : Controller
{
    private readonly MainContext _context;

    public DocumentsController(MainContext context)
    {
        _context = context;
    }
    
    // GET: api/Documents
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentModel>>> GetAllDocuments()
    {
        return await _context.Documents.ToListAsync();
    }
    
    // GET: api/Documents/2
    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentModel>> GetDocumentItem(int id)
    {
        var documentItem = await _context.Documents.FindAsync(id);
        
        if (documentItem == null)
            return NotFound();

        return documentItem;
    }
    
    // CREATE
    // POST: api/Documents
    [HttpPost]
    public async Task<ActionResult<DocumentModel>> PostDocumentItem(DocumentModel documentModel)
    {
        _context.Documents.Add(documentModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDocumentItem), new { id = documentModel.Id }, documentModel);
    }

    // UPDATE
    // POST: api/Documents/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDocumentItem(int id, DocumentModel documentItem)
    {
        if (id != documentItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(documentItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!DocumentItemExist(id))
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

    // DELETE: api/Documents/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocumentItem(int id)
    {
        var documentItem = await _context.Documents.FindAsync(id);
        if (documentItem == null)
        {
            return NotFound();
        }

        _context.Documents.Remove(documentItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DocumentItemExist(int id)
    {
        return _context.Documents.Any(e => e.Id == id);
    }
}