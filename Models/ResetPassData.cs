using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class ResetPassData
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        
    }
}
