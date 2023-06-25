using ECommerceAPI.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Data.Domain
{
    [Table("Coupon")]
    public class Coupon : BaseModel
    {
        public string Code { get; set; }
        public decimal PercentAmount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Code).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.PercentAmount).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.IsActive).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.ExpirationDate).IsRequired(true);
            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
