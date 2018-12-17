using Microsoft.EntityFrameworkCore;

namespace Users.API.Models
{
    public sealed class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
//
//            modelBuilder.Entity<User>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(10);
        }
    }
}
