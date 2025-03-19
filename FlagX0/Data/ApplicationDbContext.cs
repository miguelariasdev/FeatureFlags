using FlagX0.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlagX0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<FlagEntity> Flags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(e => e.ConcurrencyStamp).HasColumnType("longtext"); // ✅ Cambia de nvarchar(max) a longtext
            });

            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property(e => e.ConcurrencyStamp).HasColumnType("longtext"); // ✅ Cambia de nvarchar(max) a longtext
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnType("varchar(450)");
                entity.Property(e => e.RoleId).HasColumnType("varchar(450)");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnType("varchar(450)");
                entity.Property(e => e.ClaimType).HasColumnType("longtext");
                entity.Property(e => e.ClaimValue).HasColumnType("longtext");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnType("varchar(450)");
                entity.Property(e => e.ClaimType).HasColumnType("longtext");
                entity.Property(e => e.ClaimValue).HasColumnType("longtext");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(e => e.LoginProvider).HasColumnType("varchar(450)");
                entity.Property(e => e.ProviderKey).HasColumnType("varchar(450)");
                entity.Property(e => e.ProviderDisplayName).HasColumnType("longtext");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnType("varchar(450)");
                entity.Property(e => e.LoginProvider).HasColumnType("varchar(450)");
                entity.Property(e => e.Name).HasColumnType("varchar(450)");
                entity.Property(e => e.Value).HasColumnType("longtext");
            });
        }
    }
}
