using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? MaterialId { get; set; }
        public int? StandardId { get; set; }
        public int? CountryId { get; set; }
        public int? ManufacturerId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public string Tag { get; set; }
        public int? Weight { get; set; }
        public string ProductNameEN { get; set; }
        public string TagEN { get; set; }
        public string DescriptionEN { get; set; }
    }
}
