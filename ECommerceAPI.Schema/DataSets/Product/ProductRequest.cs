using ECommerceAPI.Base;

namespace ECommerceAPI.Schema.DataSets.Product
{
    public class ProductRequest : BaseRequest
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal PointEarningPercentage { get; set; }
        public decimal MaxPointAmount { get; set; }
        public List<int> CategoryIds { get; set; }
        public bool IsActive { get; set; }
    }
}
