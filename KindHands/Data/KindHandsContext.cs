using KindHands.Models;
using Microsoft.EntityFrameworkCore;

namespace KindHands.Data
{
    public class KindHandsContext : DbContext
    {
        public KindHandsContext(DbContextOptions<KindHandsContext> options) : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<Veterinarian> Veterinarians { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<VolunterAnnouncement> VolunterAnnouncements { get; set; }
        public DbSet<KindHands.Models.Shelter> Shelter { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KindHandsContext).Assembly);

        }
        
    }
}
