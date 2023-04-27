using KindHands.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindHands.Data.Configurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.ToTable(nameof(Animal));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValueSql("(getdate())");
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Age);

            builder.Property(x => x.Breed)
                .HasMaxLength(256);

            builder.Property(x => x.Kind)
                .IsRequired();

            builder.Property(x => x.Passport)
                .IsRequired();

            builder.Property(x => x.Sex)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Animals)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Shelters)
                .WithMany(s => s.Animals)
                .UsingEntity(j => j.ToTable("AnimalsShelters"));
        }
    }
}
