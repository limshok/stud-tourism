using System.ComponentModel.DataAnnotations.Schema;
using core.Models.User;

namespace core.Models;

public class LodgingModel
{
    
    public long Id { get; set; }
    public string SoloConditions { get; set; }
    public string OrgConditions { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string FoodType { get; set; }
    public int MinStayDuration { get; set; }
    public int MaxStayDuration { get; set; }
    public UniversityModel University { get; set; }
    public ContactModel? Contact { get; set; }
    public List<ImageModel>? Images { get; set; }
    public List<DocumentModel>? Documents { get; set; }
    public List<ServiceModel>? Services { get; set; }
    public List<LodgingRoomModel>? Rooms { get; set; }

    public List<MainUser> Booked { get; set; }
}