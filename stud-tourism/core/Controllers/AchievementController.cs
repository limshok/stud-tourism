using core.Data;
using core.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AchievementController : Controller
{
    private readonly MainContext _context;

    public AchievementController(MainContext context)
    {
        _context = context;
    }
    [HttpGet(nameof(GetAll))]
    public IActionResult GetAll()
    {
        return Ok(_context.Achievements.ToList());
    }
    
    [HttpGet(nameof(GetLeaders))]

    public IActionResult GetLeaders()
    {
        return Ok(GetLeadersCollection());
    }

    private IEnumerable<MainUser> GetLeadersCollection()
    {
        var c = _context.Users.ToList();
        c.Sort(((a,b) => b.Score.CompareTo(a.Score) ));
        for (int i = 0; i < 10; i++)
        {
            yield return c[i];
        }
    }
}