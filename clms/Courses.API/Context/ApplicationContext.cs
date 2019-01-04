using Courses.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.API.Context
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<Course>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Course>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Course>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Course>().Property(c => c.Description).IsRequired();
        }
    }
}
