using System.ComponentModel.DataAnnotations.Schema;
using core.Models.User;

namespace core.Models;

public class ServiceModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
}