using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class VwProductImage
    {
        public int ProductId { get; set; }
        public int? CountryId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageContent { get; set; }
        public int? ProductCategoryId { get; set; }
        public string Tag { get; set; }
        public int? Weight { get; set; }
        public string ProductNameEN { get; set; }
        public string TagEn { get; set; }
        public string DescriptionEN { get; set; }
        public int? ParentId { get; set; }
    }
}
