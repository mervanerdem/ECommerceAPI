using AutoMapper;
using ECommerceAPI.Base;
using ECommerceAPI.Data;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.Category;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Business.Services
{
    public class CategoryService : BaseService<Category, CategoryRequest, CategoryResponse>, ICategoryService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public override ApiResponse<List<CategoryResponse>> GetAll()
        {
            var categories = unitOfWork.Repository<Category>()
                    .GetAsQueryable()
                    /*.Include(x => x.Products) 
                    .ThenInclude(pc => pc.Product) */
                    .ToList();
            var categoryResponses = mapper.Map<List<CategoryResponse>>(categories);
            return new ApiResponse<List<CategoryResponse>>(categoryResponses);
        }

        public override ApiResponse<CategoryResponse> GetById(int id)
        {
            var category = unitOfWork.Repository<Category>()
                    .Where(x => x.Id.Equals(id))
                    /*.Include(x => x.Products)
                    .ThenInclude(pc => pc.Product)*/
                    .FirstOrDefault();
            if (category is null)
            {
                return new ApiResponse<CategoryResponse>("Not Found");
            }
            var categoryResponse = mapper.Map<CategoryResponse>(category);
            return new ApiResponse<CategoryResponse>(categoryResponse);
        }
    }
}
