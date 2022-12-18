using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBean.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var appUserIDsConverter = new ValueConverter<IEnumerable<string>, string>(// конвертируем из тегов в строку
        //    v => string.Join(";", v), v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).AsEnumerable()); // конвертируем из строки в теги

        //    var appUserIDsValueComparer = new ValueComparer<IEnumerable<string>>(
        //    (x, y) => x.SequenceEqual(y, StringComparer.OrdinalIgnoreCase),// переопределяем Equals
        //     x => x.Aggregate(0,(a, v) => HashCode.Combine(a,v.GetHashCode(StringComparison.OrdinalIgnoreCase))),// переопределяем GetHashCode
        //     x => x.ToArray());// специальное выражение для создания снепшота данных

        //    modelBuilder
        //        .Entity<AppUser>()
        //        .Property(p => p.WishListIDs)
        //        .HasConversion(appUserIDsConverter, appUserIDsValueComparer);

        //    modelBuilder
        //        .Entity<AppUser>()
        //        .Property(p => p.BusketIDs)
        //        .HasConversion(appUserIDsConverter, appUserIDsValueComparer);
        //}
    }
}
