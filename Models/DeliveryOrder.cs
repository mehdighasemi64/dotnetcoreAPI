using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class DeliveryOrder
    {
        public int DeliveryOrderId { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public DateTime? DesireDateTime { get; set; }
        public string Description { get; set; }
    }
}
