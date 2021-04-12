using System;
using System.Collections.Generic;

#nullable disable

namespace OAuth.Data.Contract.Models
{
    public partial class TokenManager
    {
        public int Id { get; set; }
        public string RefreshTokenSecret { get; set; }
        public string AccessTokenSecret { get; set; }
        public DateTime RefreshTokenUpdateAt { get; set; }
        public DateTime AccessTokenUpdateAt { get; set; }
    }
}
