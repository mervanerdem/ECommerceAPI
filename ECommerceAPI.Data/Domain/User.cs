using ECommerceAPI.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Data
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public decimal DigitalWallet { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(p => p.CreatedAt).IsRequired(false);
            builder.Property(p => p.CreatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(p => p.UpdatedAt).IsRequired(false);
            builder.Property(p => p.UpdatedBy).IsRequired(false).HasMaxLength(30);

            builder.Property(p => p.UserName).IsRequired(true).HasMaxLength(30);
            builder.Property(p => p.Email).IsRequired(true).HasMaxLength(30);
            builder.Property(p => p.Password).IsRequired(true).HasMaxLength(100);
            builder.Property(p => p.FirstName).IsRequired(true).HasMaxLength(30);
            builder.Property(p => p.LastName).IsRequired(true).HasMaxLength(30);
            builder.Property(p => p.Role).IsRequired(true).HasMaxLength(10).HasDefaultValue("Customer");
            builder.Property(p => p.DigitalWallet).HasDefaultValue(0);
            builder.Property(x => x.Status).IsRequired(true);

            builder.HasIndex(x => x.UserName).IsUnique(true);

            Seed(builder);
        }

        //Default veritabanı oluştururken yapılacak işlem
        private void Seed(EntityTypeBuilder<User> builder)
        {
            var existingUser = builder.HasData(new User
            {
                Id = 1,
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@dartvader.com",
                Password = "5f4dcc3b5aa765d61d8327deb882cf99", // password
                Role = "Admin",
                Status = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = "Migrations",
                UpdatedBy = "Migrations"
            });
        }
    }
}
