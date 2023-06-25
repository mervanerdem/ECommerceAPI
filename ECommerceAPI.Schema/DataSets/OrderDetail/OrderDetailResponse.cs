using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Schema.DataSets.OrderDetail
{
    public class OrderDetailResponse
    {
        public string OrderNumber { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
