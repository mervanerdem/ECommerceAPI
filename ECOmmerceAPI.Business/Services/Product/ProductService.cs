using AutoMapper;
using ECommerceAPI.Base;
using ECommerceAPI.Data;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.Product;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Business.Services
{
    public class ProductService : BaseService<Product, ProductRequest, ProductResponse>, IProductService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public override ApiResponse Insert(ProductRequest request)
        {

            var product = mapper.Map<Product>(request);
            var categoryIds = request.CategoryIds;

            var categories = unitOfWork.Repository<Category>().Where(p => categoryIds.Contains(p.Id));

            foreach (var category in categories)
            {
                product.Categories.Add(new ProductsCategories
                {
                    Product = product,
                    Category = category,
                    CreatedBy = "Admin",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "Admin"
            });
            }
            product.CreatedBy = "Admin";
            product.CreatedAt = DateTime.Now;

            unitOfWork.Repository<Product>().Add(product);
            if (unitOfWork.Complete() > 0)
            {
                return new ApiResponse();

            }
            return new ApiResponse("Internal Server Error");

        }

        public override ApiResponse Update(int id, ProductRequest request)
        {
            var productToUpdate = unitOfWork.Repository<Product>().Where(p => p.Id.Equals(id))?.Include(p => p.Categories)?.FirstOrDefault();
            if (productToUpdate == null)
            {
                return new ApiResponse("Product not found");
            }

            mapper.Map(request, productToUpdate);


            var updatedCategoryIds = request.CategoryIds;
            var existingCategoryIds = productToUpdate.Categories.Select(p => p.CategoryId).ToList();

            var categoriesToAdd = updatedCategoryIds.Except(existingCategoryIds).ToList();


            var categoriesToRemove = existingCategoryIds.Except(updatedCategoryIds).ToList();

            foreach (var categoryId in categoriesToAdd)
            {
                productToUpdate.Categories.Add(new ProductsCategories
                {
                    CategoryId = categoryId,
                    CreatedBy = "Admin",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "Admin"
                });
            }

            foreach (var categoryId in categoriesToRemove)
            {
                var productCategoryMapToRemove = productToUpdate.Categories.FirstOrDefault(p => p.CategoryId == categoryId);
                if (productCategoryMapToRemove != null)
                {
                    productToUpdate.Categories.Remove(productCategoryMapToRemove);
                }
            }

            var categoriesToRemoveCompletely = unitOfWork.Repository<ProductsCategories>().Where(p => p.ProductId == id && !updatedCategoryIds.Contains(p.CategoryId)).ToList();
            foreach (var categoryToRemoveCompletely in categoriesToRemoveCompletely)
            {
                unitOfWork.Repository<ProductsCategories>().Delete(categoryToRemoveCompletely);
            }

            unitOfWork.Repository<Product>().Update(productToUpdate);
            if (unitOfWork.Complete() > 0)
            {
                return new ApiResponse();
            }
            return new ApiResponse("Internal Server Error");

        }
        public ApiResponse<List<ProductResponse>> GetAllProduct(bool isActive)
        {
            var products = unitOfWork.Repository<Product>().Where(p => p.IsActive.Equals(isActive))/*.Include(p => p.Categories).ThenInclude(p => p.Category).IgnoreAutoIncludes()*/.ToList();

            var productsResponse = mapper.Map<List<ProductResponse>>(products);

            return new ApiResponse<List<ProductResponse>>(productsResponse);
        }

        public override ApiResponse<List<ProductResponse>> GetAll()
        {
            var products = unitOfWork.Repository<Product>().GetAll()/*.Include(p => p.Categories).ThenInclude(p => p.Category)*/.ToList();
            var productsResponse = mapper.Map<List<ProductResponse>>(products);
            return new ApiResponse<List<ProductResponse>>(productsResponse);
        }

        public override ApiResponse<ProductResponse> GetById(int id)
        {
            var product = unitOfWork.Repository<Product>().Where(p => p.Id.Equals(id))/*?.Include(p => p.Categories).ThenInclude(p => p.Category)*/.FirstOrDefault();
            if (product is null)
            {
                return new ApiResponse<ProductResponse>("Not Found");
            }
            var productResponse = mapper.Map<ProductResponse>(product);
            return new ApiResponse<ProductResponse>(productResponse);
        }

        public override ApiResponse Delete(int id)
        {
            var productToDelete = unitOfWork.Repository<Product>().GetById(id);
            if (productToDelete == null)
            {
                return new ApiResponse("Product not found");
            }

            var productCategoriesToRemove = unitOfWork.Repository<ProductsCategories>().Where(p => p.ProductId == id).ToList();
            foreach (var productCategoryMap in productCategoriesToRemove)
            {
                unitOfWork.Repository<ProductsCategories>().Delete(productCategoryMap);
            }
            unitOfWork.Repository<Product>().Delete(productToDelete);

            if (unitOfWork.Complete() > 0)
            {
                return new ApiResponse();
            }
            return new ApiResponse("Internal Server Error");
        }

        public ApiResponse UpdateStock(int id, int stock)
        {
            var product = unitOfWork.Repository<Product>().GetByIdAsNoTracking(id);
            if (product is null)
            {
                return new ApiResponse("Product Not Found.");
            }
            product.Stock = stock;
            unitOfWork.Repository<Product>().Update(product);
            if (unitOfWork.Complete() > 0)
            {
                return new ApiResponse();
            }
            return new ApiResponse("Internal Server Error");
        }
    }
}
