using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.Category;

namespace ECommerceAPI.Business.Services
{
    public interface ICategoryService : IBaseService<Category, CategoryRequest, CategoryResponse>
    {
    }
}
