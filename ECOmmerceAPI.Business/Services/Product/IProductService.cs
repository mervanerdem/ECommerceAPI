using ECommerceAPI.Base;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.Product;

namespace ECommerceAPI.Business.Services
{
    public interface IProductService : IBaseService<Product, ProductRequest, ProductResponse>
    {
        public ApiResponse UpdateStock(int id, int stock);

        public ApiResponse<List<ProductResponse>> GetAllProduct(bool isActive);
    }
}
