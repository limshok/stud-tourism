using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models;

public class ServiceModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    [Column(TypeName = "text")]
    public string Description { get; set; }
    public int Cost { get; set; }
}