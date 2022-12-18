using CoffeeBean.Entity;
using CoffeeBean.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeBean.DataAccess
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.WishList)
                    .HasForeignKey(d => d.AppUserId);
            });


            //var appUserIDsConverter = new ValueConverter<IEnumerable<string>, string>(// конвертируем из тегов в строку
            //v => string.Join(";", v), v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).AsEnumerable()); // конвертируем из строки в теги

            //var appUserIDsValueComparer = new ValueComparer<IEnumerable<string>>(
            //(x, y) => x.SequenceEqual(y, StringComparer.OrdinalIgnoreCase),// переопределяем Equals
            // x => x.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode(StringComparison.OrdinalIgnoreCase))),// переопределяем GetHashCode
            // x => x.ToArray());// специальное выражение для создания снепшота данных

            //modelBuilder
            //    .Entity<AppUser>()
            //    .Property(p => p.WishListIDs)
            //    .HasConversion(appUserIDsConverter, appUserIDsValueComparer);

            //modelBuilder
            //    .Entity<AppUser>()
            //    .Property(p => p.BusketIDs)
            //    .HasConversion(appUserIDsConverter, appUserIDsValueComparer);
        }
    }
}
