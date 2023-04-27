using KindHands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindHands.Data.Configurations
{
    public class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
    {
        public void Configure(EntityTypeBuilder<Shelter> builder)
        {
            builder.ToTable(nameof(Shelter));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValueSql("getdate()");
            builder.Property(x => x.Id).UseIdentityColumn();

            //builder.HasMany(x => x.Animals)
            //    .WithOne(x => x.)
        }
    }
}
