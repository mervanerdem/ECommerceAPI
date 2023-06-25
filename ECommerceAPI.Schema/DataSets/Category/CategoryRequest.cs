using ECommerceAPI.Base;
using System;
using System.Linq;

namespace ECommerceAPI.Schema.DataSets.Category
{
    public class CategoryRequest : BaseRequest
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Tags { get; set; }
    }
}
