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

            base.OnModelCreating(modelBuilder);
        }
    }
}
