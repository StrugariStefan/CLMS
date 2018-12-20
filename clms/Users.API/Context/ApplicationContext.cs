using Microsoft.EntityFrameworkCore;
using Users.API.Models;

namespace Users.API.Context
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Student>().HasBaseType<User>();
            modelBuilder.Entity<Teacher>().HasBaseType<User>();
        }
    }
}