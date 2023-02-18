using core.Data;
using core.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers;

[Route("api/{controller}")]
[ApiController]
public class AchievementController : Controller
{
    private readonly MainContext _context;

    public AchievementController(MainContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Achievements.ToList());
    }

    public IActionResult GetLeaders()
    {
        return Ok(GetLeadersCollection());
    }

    private IEnumerable<MainUser> GetLeadersCollection()
    {
        _context.Users.OrderBy(u => u.Score);
        for (int i = 0; i < 10; i++)
        {
            yield return _context.Users.ElementAt(i);
        }
    }
}