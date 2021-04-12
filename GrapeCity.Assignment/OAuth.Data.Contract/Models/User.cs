using System;
using System.Collections.Generic;

#nullable disable

namespace OAuth.Data.Contract.Models
{
    public partial class User
    {
        public User()
        {
            UserCredentials = new HashSet<UserCredential>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Intro { get; set; }
        public string Profile { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<UserCredential> UserCredentials { get; set; }
    }
}
