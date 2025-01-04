using Microsoft.EntityFrameworkCore;
using MVCWebSite.Data.Map;
using MVCWebSite.Models;

namespace MVCWebSite.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base (options)
        {                  
        }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<UserModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
