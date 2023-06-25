using ECommerceAPI.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Data.Domain
{
    public class OrderDetail : BaseModel
    {
        public string OrderNumber { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.OrderNumber).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.ProductName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.ProductId).IsRequired(true);
        }
    }
}
