using KindHands.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KindHands.Data.Configurations
{
    class VeterinarianConfiguration : IEntityTypeConfiguration<Veterinarian>
    {
        public void Configure(EntityTypeBuilder<Veterinarian> builder)
        {
            builder.ToTable(nameof(Veterinarian));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValueSql("(getdate())");
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Clinic)
                .WithMany(x => x.Veterinarians)
                .HasForeignKey(x => x.ClinicId)
                .OnDelete(DeleteBehavior.Restrict)
            ;
        }
    }
}
