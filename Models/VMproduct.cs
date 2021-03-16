using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class VMproduct
    {
        public int OrderId { get; set; }
        public Product[] products { get; set; }
    }
}
