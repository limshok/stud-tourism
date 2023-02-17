using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using core.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace core.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : Controller
{
    private readonly SignInManager<MainUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthorizationController(SignInManager<MainUser> signInManager,IConfiguration configuration)
    {
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<IActionResult> Register(string username,string firstName,string lastName, string pass)
    {
        var user = await _signInManager.UserManager.FindByNameAsync(username);
        if (user != null)
        {
            BadRequest("Username is busy");
        }

        user = new MainUser()
        {
            UserName = username,
            FirstName = firstName,
            LastName = lastName
        };
        await _signInManager.UserManager.CreateAsync(user, pass);
        return await Authorize(username, pass);
    }

    [HttpPost]
    [Route(nameof(Authorize))]

    public async Task<IActionResult> Authorize(string name,string pass)
    {
        var user = await _signInManager.UserManager.FindByNameAsync(name);
        if (user == null)
        {
            return NotFound();
        }
        
        var result = await _signInManager.PasswordSignInAsync(user, pass, false, false);

        if (result.Succeeded)
        {
            byte[] secretBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString())
                },
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(value);
        }
        return BadRequest();
    }

    [Authorize]
    [HttpGet(nameof(Try))]
    public IActionResult Try()
    {
        // var id = HttpContext.User.Claims.First(c => c.Type == "Id");

        return Ok("Hello");
        // return Ok(identity.FirstName + " " + identity.LastName);
    }
}