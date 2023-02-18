using core.Models.User;

namespace core.Models;

public class MessageModel
{
    public ulong Id { get; set; }
    public List<MainUser> MainUsers { get; set; }
    public string SenderUserName { get; set; }
    public string Text { get; set; }
}