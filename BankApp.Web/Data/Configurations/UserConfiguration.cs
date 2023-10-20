using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Web.Data.Entities;

namespace BankApp.Web.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Name).HasMaxLength(20);
            builder.Property(u => u.Surname).IsRequired();
            builder.Property(u => u.Surname).HasMaxLength(20);
            builder.HasMany(u => u.Accounts).WithOne(a => a.User).HasForeignKey(a => a.UserId);
            
        }
    }
}
