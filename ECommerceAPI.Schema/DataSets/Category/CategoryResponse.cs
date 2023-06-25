using ECommerceAPI.Base;
using ECommerceAPI.Data.Domain;
using System;
using System.Linq;

namespace ECommerceAPI.Schema.DataSets.Category
{
    public class CategoryResponse : BaseResponse
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Tags { get; set; }
        public virtual ICollection<ProductsCategories> Products { get; set; }
    }
}
