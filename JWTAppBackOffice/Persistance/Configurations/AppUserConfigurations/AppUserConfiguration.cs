using JWTAppBackOffice.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWTAppBackOffice.Persistance.Configurations.AppUserConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(x => x.AppRole).WithMany(x => x.AppUsers).HasForeignKey(x => x.AppRoleId);

            //builder.HasMany(x => x.AppUsers).WithOne(x => x.AppRole).HasForeignKey(x => x.AppRoleId);

        }
    }
}
