using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class Shop
    {
        public int ShopId { get; set; }
        public int? OrderId { get; set; }
        public DateTime? DateShop { get; set; }
        public long? Price { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? Status { get; set; }
        public string Authority { get; set; }
        public string RefId { get; set; }
        public string Description { get; set; }
    }
}
