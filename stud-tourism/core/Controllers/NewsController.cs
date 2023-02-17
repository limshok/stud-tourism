using core.Data;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : Controller
{
    private readonly MainContext _context;

    public NewsController(MainContext context)
    {
        _context = context;
    }

    // GET: api/News
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NewsModel>>> GetAllNews()
    {
        return await _context.News.ToListAsync();
    }

    // GET: api/News/2
    [HttpGet("{id}")]
    public async Task<ActionResult<NewsModel>> GetNewsItem(int id)
    {
        var newsItem = await _context.News.FindAsync(id);

        if (newsItem == null)
            return NotFound();

        return newsItem;
    }

    // CREATE
    // POST: api/News
    [HttpPost]
    public async Task<ActionResult<NewsModel>> PostNewsItem(NewsModel newsModel)
    {
        _context.News.Add(newsModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNewsItem), new { id = newsModel.Id }, newsModel);
    }

    // UPDATE
    // POST: api/News/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNewsItem(int id, NewsModel newsItem)
    {
        if (id != newsItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(newsItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (!NewsItemExist(id))
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

    // DELETE: api/News/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNewsItem(int id)
    {
        var newsItem = await _context.News.FindAsync(id);
        if (newsItem == null)
        {
            return NotFound();
        }

        _context.News.Remove(newsItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NewsItemExist(int id)
    {
        return _context.News.Any(e => e.Id == id);
    }
}