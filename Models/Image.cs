using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public int? ImageType { get; set; }
        
        [NotMapped]
        public string Content { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImageFormat { get; set; }
    }
}
