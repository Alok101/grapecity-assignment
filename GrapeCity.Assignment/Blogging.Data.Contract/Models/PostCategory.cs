using System;
using System.Collections.Generic;

#nullable disable

namespace Blogging.Data.Contract.Models
{
    public partial class PostCategory
    {
        public long PostId { get; set; }
        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Post Post { get; set; }
    }
}
