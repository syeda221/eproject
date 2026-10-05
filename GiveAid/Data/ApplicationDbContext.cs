using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiveAid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GiveAid.Models.Cause> Causes { get; set; }
        public DbSet<GiveAid.Models.Ngo> NGOs { get; set; }
        public DbSet<GiveAid.Models.Donation> Donations { get; set; }
        public DbSet<GiveAid.Models.Programme> Programmes { get; set; }
        public DbSet<GiveAid.Models.Query> Queries { get; set; }
        public DbSet<GiveAid.Models.Gallery> Galleries { get; set; }
    }
}