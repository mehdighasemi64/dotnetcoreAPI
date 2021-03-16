using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace dotnetcoreAPI.Models
{
    public partial class VMproductImage
    {
        public Image image { get; set; }
        public Product product { get; set; }
        // public IFormFile image { get; set; }
        // public Product product { get; set; }
    }
}
