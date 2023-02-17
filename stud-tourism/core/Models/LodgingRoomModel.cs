using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models;

public class LodgingRoomModel
{
    public long Id { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public int Cost { get; set; }
    [Column(TypeName = "text")]
    public string Description { get; set; }
    public List<ImageModel>? Images { get; set; }
}