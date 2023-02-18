using System.ComponentModel.DataAnnotations.Schema;
using core.Models.User;

namespace core.Models;

public class EventModel
{
    public long Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public string StartDate { get; set; }
    public string FinishDate { get; set; }
    public UniversityModel University { get; set; }
    public List<ImageModel>? Images { get; set; }
    public List<MainUser> FollowedUsers { get; set; }

}