using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Count { get; set; }
        public long? Price { get; set; }
        public int? Discount { get; set; }
        public long? TotalCost { get; set; }
    }
}
