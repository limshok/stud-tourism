using System.Security.Claims;
using core.Data;
using core.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers;

[Route("api/{controller}")]
[ApiController]
public class MessagesController : Controller
{
    private readonly SignInManager<MainUser> _signInManager;
    private readonly MainContext _context;

    public MessagesController(SignInManager<MainUser> signInManager,MainContext context)
    {
        _signInManager = signInManager;
        _context = context;
    }
    public async Task<IActionResult> GetMessages()
    {
        var id = HttpContext.User.Claims.First(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
        var user = await _signInManager.UserManager.FindByNameAsync(id.Value);
        return Ok(_context.Messages.Where(m => (m.MainUsers[0].UserName == user.UserName || m.MainUsers[1].UserName == user.UserName)));
    }
}