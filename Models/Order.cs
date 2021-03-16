using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderStatusId { get; set; }
        public long? Cost { get; set; }
        public int? Discount { get; set; }
        public long? TotalCost { get; set; }
        public int? TotalQuantity { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryEmail { get; set; }
    }
}
