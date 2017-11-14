using System;
using Exodus3.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exodus3.Api.Data
{
    public class E3DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Sermon> Sermons { get; set; }
        public DbSet<Series> Series { get; set; }

        public E3DbContext(DbContextOptions<E3DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
