using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exodus3.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exodus3.Api.Data
{
    public class E3DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Sermon> Sermons { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }

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

            builder.Entity<Season>()
               .HasIndex(x => new { x.SeriesId, x.Number})
               .IsUnique();

            builder.Entity<E3Entity>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<E3Entity>().Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }

            foreach (var item in ChangeTracker.Entries<E3Entity>()
                     .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
            {
                if (item.State == EntityState.Added)
                    item.CurrentValues["CreatedOn"] = new DateTimeOffset(DateTime.UtcNow);

                item.CurrentValues["UpdatedOn"] = new DateTimeOffset(DateTime.UtcNow);
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
