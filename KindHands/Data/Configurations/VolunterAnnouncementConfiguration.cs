using KindHands.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KindHands.Data.Configurations
{
    class VolunterAnnouncementConfiguration : IEntityTypeConfiguration<VolunterAnnouncement>
    {
        public void Configure(EntityTypeBuilder<VolunterAnnouncement> builder)
        {
            builder.ToTable(nameof(VolunterAnnouncement));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValueSql("(getdate())");
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.User)
                .WithMany(x => x.VolunterAnnouncements)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict)
            ;
        }
    }
}
