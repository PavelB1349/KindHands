using KindHands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindHands.Data.Configurations
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable(nameof(Advertisement));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValueSql("(getdate())");
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Advertisements)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict)
            ;

            builder.HasOne(x => x.Animal)
                .WithMany(x => x.Advertisements)
                .HasForeignKey(x => x.AnimalId)
                .OnDelete(DeleteBehavior.Restrict)
            ;
        }
    }
}
