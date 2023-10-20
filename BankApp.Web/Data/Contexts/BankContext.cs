using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using BankApp.Web.Data.Configurations;
using BankApp.Web.Data.Entities;

namespace BankApp.Web.Data.Contexts
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
        }
    }
}
