using core.Models.User;

namespace core.Models;

public class AchievementModel
{
    public long Id { get; set; }
    public string Url { get; set; }
    public int Score { get; set; }
    public List<MainUser> Users { get; set; }
}