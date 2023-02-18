using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models;

public class ScienceModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public UniversityModel University { get; set; }
    public ContactModel? ContactModel { get; set; }
    public int FoundationDate { get; set; } 
    public List<ImageModel>? Images { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
}