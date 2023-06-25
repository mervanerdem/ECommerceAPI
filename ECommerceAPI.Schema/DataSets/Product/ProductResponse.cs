using ECommerceAPI.Base;
using ECommerceAPI.Data.Domain;
using System;
using System.Linq;

namespace ECommerceAPI.Schema.DataSets.Product
{
    public class ProductResponse : BaseResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal PointEarningPercentage { get; set; }
        public decimal MaxPointAmount { get; set; }
        public virtual ICollection<ProductsCategories> Categories { get; set; }
        public bool IsActive { get; set; }
    }
}
