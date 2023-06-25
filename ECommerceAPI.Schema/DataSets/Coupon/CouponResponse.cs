using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Schema.DataSets.Coupon
{
    public class CouponResponse
    {
        public string Code { get; set; }
        public decimal PercentAmount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
