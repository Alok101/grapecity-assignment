using System;
using System.Collections.Generic;

#nullable disable

namespace OAuth.Data.Contract.Models
{
    public partial class UserCredential
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EncriptionKey { get; set; }
        public DateTime RegisterAt { get; set; }
        public bool? IsActive { get; set; }
        public long? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
