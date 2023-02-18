using System.Security.Claims;
using core.Data;
using core.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : Controller
{
    private readonly MainContext _context;
    private readonly SignInManager<MainUser> _signInManager;

    public BookingController(MainContext context,SignInManager<MainUser> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }
    
    [HttpPost(nameof(GetBookings))]
    [Authorize]
    public async Task<IActionResult> GetBookings()
    {
        var id = HttpContext.User.Claims.First(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
        var user = await _signInManager.UserManager.FindByNameAsync(id.Value);
        return Ok(user.Bookings);
    }

    [HttpPost(nameof(AddBooking))]
    [Authorize]
    public async Task<IActionResult> AddBooking(long lodgingId)
    {
        var id = HttpContext.User.Claims.First(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
        var user = await _signInManager.UserManager.FindByNameAsync(id.Value);
        var lodging =  await  _context.Lodgings.FirstOrDefaultAsync(l => l.Id == lodgingId);

        if (user != null && lodging != default)
        {
            user.Bookings.Add(lodging);
            return Ok(user.Bookings);
        }
        return BadRequest();
    }
    
    [HttpDelete(nameof(RemoveBooking))]
    [Authorize]
    public async Task<IActionResult> RemoveBooking(long lodgingId)
    {
        var id = HttpContext.User.Claims.First(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
        var user = await _signInManager.UserManager.FindByNameAsync(id.Value);
        var lodging =  await  _context.Lodgings.FirstOrDefaultAsync(l => l.Id == lodgingId);

        if (user != null && lodging != default)
        {
            user.Bookings.Remove(lodging);
            return Ok(user.Bookings);
        }
        return BadRequest();
    }
}