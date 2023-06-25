using ECommerceAPI.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace ECommerceAPI.Data.Domain
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Tags { get; set; }
        public virtual ICollection<ProductsCategories> Products { get; set; }
        
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.URL).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Tags).IsRequired(true).HasMaxLength(150);
        }
    }
}
