using Gamification.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamification.API.Context
{
    public sealed class ApplicationContext : DbContext
    {
        /// <inheritdoc />
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Question> Questions { get; private set; }
        public DbSet<Answer> Answers { get; private set; }
        public DbSet<Score> Scores { get; private set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Score>()
                .HasKey(s => new {s.CourseId, s.UserId});
            modelBuilder.Entity<Score>().Property(s => s.ActualScore).IsRequired();

            modelBuilder.Entity<Question>().Property(q => q.Id).IsRequired();
            modelBuilder.Entity<Question>().HasIndex(q => q.Id).IsUnique();
            modelBuilder.Entity<Question>().Property(q => q.CourseId).IsRequired();
            modelBuilder.Entity<Question>().Property(q => q.CreatedBy).IsRequired();
            modelBuilder.Entity<Question>().Property(q => q.ActualQuestion).IsRequired();
            modelBuilder.Entity<Question>().Property(q => q.LevelOfInterest).IsRequired();
            modelBuilder.Entity<Question>().Property(q => q.Type).IsRequired();

            modelBuilder.Entity<Answer>().Property(a => a.Id).IsRequired();
            modelBuilder.Entity<Answer>().HasIndex(a => a.Id).IsUnique();
            modelBuilder.Entity<Answer>().Property(a => a.ActualAnswer).IsRequired();
            modelBuilder.Entity<Answer>().Property(a => a.CreatedBy).IsRequired();
            modelBuilder.Entity<Answer>().Property(a => a.QuestionId).IsRequired();
        }
    }
}
