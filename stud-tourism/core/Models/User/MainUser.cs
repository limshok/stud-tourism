using Microsoft.AspNetCore.Identity;

namespace core.Models.User;

public class MainUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int Score { get; set; }
    
    public List<AchievementModel> AchievementModels { get; set; }
    public List<EventModel> Follows { get; set; }
    public List<MessageModel> Messages { get; set; }
    public List<LodgingModel> Bookings { get; set; }
}