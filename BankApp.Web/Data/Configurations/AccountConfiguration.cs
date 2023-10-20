using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Web.Data.Entities;

namespace BankApp.Web.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.AccountNumber).IsRequired();
            builder.Property(a => a.Balance).IsRequired();
            builder.Property(a => a.Balance).HasColumnType("decimal(18,4)");
        }
    }
}
