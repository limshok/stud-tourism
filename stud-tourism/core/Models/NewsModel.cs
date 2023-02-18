using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models;

public class NewsModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<HashtagModel> Hashtags { get; set; }
    public List<ImageModel> Images { get; set; }
}