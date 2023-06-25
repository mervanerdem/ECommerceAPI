using ECommerceAPI.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Data.Domain
{
    public class ProductsCategories :BaseModel
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }

    public class ProductsCategoriesConfiguration : IEntityTypeConfiguration<ProductsCategories>
    {
        public void Configure(EntityTypeBuilder<ProductsCategories> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.HasOne(pc => pc.Product)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(pc => pc.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pc => pc.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(pc => pc.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
