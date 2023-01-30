using CoffeeBean.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBean.DataAccess
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(x => new { x.LoginProvider, x.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            modelBuilder.Entity<AppUser>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.AppUser)
                .WithMany(a => a.Cart)
                .HasForeignKey(p => p.AppUserId);


        }
    }
}
