using core.Models;
using core.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace core.Data;

public class MainContext : IdentityDbContext<MainUser,CustomRole,Guid>
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
        
    }

    public DbSet<ContactModel> Contacts{ get; set; }
    public DbSet<LodgingModel> Lodgings{ get; set; }
    public DbSet<ScienceModel> Sciences{ get; set; }
    public DbSet<ServiceModel> Services{ get; set; }
    public DbSet<EventModel> Events{ get; set; }
    public DbSet<UniversityModel> Universities{ get; set; }
    public DbSet<LodgingRoomModel> LodgingRooms{ get; set; }
    public DbSet<ImageModel> Images{ get; set; }
    public DbSet<DocumentModel> Documents{ get; set; }
    public DbSet<NewsModel> News{ get; set; }
    public DbSet<HashtagModel> Hashtags{ get; set; }
}