using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ParentId { get; set; }
        public string ProductCategoryNameEN { get; set; }
    }
}
