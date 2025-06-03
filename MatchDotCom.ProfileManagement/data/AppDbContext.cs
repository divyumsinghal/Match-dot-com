using Microsoft.EntityFrameworkCore;

namespace MatchDotCom.ProfileManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserProfile.UserProfile> UserProfiles { get; set; }
    }
}