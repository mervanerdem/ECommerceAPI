using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Data
{
    public class EFContext : DbContext
    {
        //En sevdiğimiz yer Entity Framework kısmı
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        //DBSet(Database Set) kısmı burda tablolarımızı oluşturyoruz.

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new UserLogConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.ApplyConfiguration(new AccountConfiguration());
            //modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            //modelBuilder.ApplyConfiguration(new TransactionViewConfiguration());
            //modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

            //modelBuilder.Entity<TransactionView>().Metadata.SetIsTableExcludedFromMigrations(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
