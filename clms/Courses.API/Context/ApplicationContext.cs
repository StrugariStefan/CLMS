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
        public DbSet<ResourceFile> ResourceFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<Course>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Course>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Course>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Course>().Property(c => c.Description).IsRequired();

            modelBuilder.Entity<ResourceFile>().Property(r => r.Id).IsRequired();
            modelBuilder.Entity<ResourceFile>().HasIndex(r => r.Id).IsUnique();
            modelBuilder.Entity<ResourceFile>().Property(r => r.Name).IsRequired();
            modelBuilder.Entity<ResourceFile>().Property(r => r.Description).IsRequired();
            modelBuilder.Entity<ResourceFile>().Property(r => r.Type).IsRequired();
            modelBuilder.Entity<ResourceFile>().Property(r => r.CourseId).IsRequired();
            modelBuilder.Entity<ResourceFile>()
                .HasOne(r => r.Course)
                .WithMany(c => c.ResourceFiles)
                .HasForeignKey(r => r.CourseId)
                .HasConstraintName("ForeignKey_Course_ResourceFile");
        }
    }
}
