using Microsoft.AspNetCore.Identity;

namespace core.Models.User;

public class MainUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}