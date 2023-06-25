using ECommerceAPI.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ECommerceAPI.Data
{
    public class EFContext : DbContext
    {
        //En sevdiğimiz yer Entity Framework kısmı
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        //DBSet(Database Set) kısmı burda tablolarımızı oluşturyoruz.

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsCategories> ProductCategories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
