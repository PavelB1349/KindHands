using KindHands.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KindHands.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValueSql("(getdate())");
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
