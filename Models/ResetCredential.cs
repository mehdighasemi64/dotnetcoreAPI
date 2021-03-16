using System;
using System.Collections.Generic;

#nullable disable

namespace dotnetcoreAPI.Models
{
    public partial class ResetCredential
    {
        public int ResetCredentialId { get; set; }
        public string UserName { get; set; }
        public string TokenHash { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? TokenUsed { get; set; }
    }
}
