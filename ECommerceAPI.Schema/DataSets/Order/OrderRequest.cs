using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Schema.DataSets.Order
{
    public class OrderRequest
    {
        public string CardNumber { get; set; }
        public string CardDate { get; set; }
        public string CardName { get; set; }
        public string CVV { get; set; }
        public string CouponCode { get; set; }
        public List<int> ProductIds { get; set; }

    }
}
