using ECommerceAPI.Base;
using ECommerceAPI.Business.Services;
using ECommerceAPI.Schema.DataSets.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Service.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllProduct")]
        public ApiResponse<List<ProductResponse>> GetAllProduct(bool isActive)
        {
            var response = productService.GetAllProduct(isActive);
            return response;
        }

        [HttpGet("GetById")]
        public ApiResponse<ProductResponse> GetById(int id)
        {
            var response = productService.GetById(id);
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ApiResponse AddProduct(ProductRequest categoryRequest)
        {
            var response = productService.Insert(categoryRequest);
            return response;
        }

        [HttpPut("UpdateProduct")]
        [Authorize(Roles = "Admin")]
        public ApiResponse UpdateProduct(int id, ProductRequest categoryRequest)
        {
            var response = productService.Update(id, categoryRequest);
            return response;
        }

        [HttpPut("StockUpdate")]
        [Authorize(Roles = "Admin")]
        public ApiResponse StockUpdate(int id, int stock)
        {
            var response = productService.UpdateStock(id, stock);
            return response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ApiResponse DeleteProduct(int id)
        {
            var response = productService.Delete(id);
            return response;
        }

    }
}
