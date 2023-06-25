using ECommerceAPI.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Data.Domain
{
    public class Product : BaseModel
    {
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int PointEarningPercentage { get; set; }
        public decimal MaxPointAmount { get; set; }
        public virtual ICollection<ProductsCategories> Categories { get; set; } = new List<ProductsCategories>();
        public bool IsActive { get; set; }

    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired(true);
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.MaxPointAmount).IsRequired(true);
            builder.Property(x => x.PointEarningPercentage).IsRequired(true);
        }
    }
}
