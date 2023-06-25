using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Schema.DataSets.Order
{
    public class OrderResponse
    {
        public int UserID { get; set; }
        public string OrderNumber { get; set; }
        public decimal CartAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointAmount { get; set; }
    }
}
