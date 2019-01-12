using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Context
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Token> Tokens { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>()
                .HasKey(t => new {t.ActualToken, t.UserId});
        }
    }
}
