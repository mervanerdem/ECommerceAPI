using ECommerceAPI.Base;
using ECommerceAPI.Business.Services;
using ECommerceAPI.Schema.DataSets.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECommerceAPI.Service.Contollers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public ApiResponse<List<CategoryResponse>> GetAll()
        {
            var response = categoryService.GetAll();
            return response;
        }

        [HttpGet("GetById")]
        public ApiResponse<CategoryResponse> GetById(int id)
        {
            var response = categoryService.GetById(id);
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ApiResponse AddCategory(CategoryRequest categoryRequest)
        {
            var response = categoryService.Insert(categoryRequest);
            return response;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ApiResponse UpdateCategory(int id, CategoryRequest categoryRequest)
        {
            var response = categoryService.Update(id, categoryRequest);
            return response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ApiResponse DeleteCategory(int id)
        {
            var response = categoryService.Delete(id);
            return response;
        }


    }
}
