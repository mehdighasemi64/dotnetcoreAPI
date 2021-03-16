using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class VwShoppingHistoryOld
    {
        public int? OrderId { get; set; }
        public int? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Description { get; set; }
        public long? Price { get; set; }
        public int? OrderStatusId { get; set; }
        public int ShopId { get; set; }
        public string OrderStatusName { get; set; }
    }
}
