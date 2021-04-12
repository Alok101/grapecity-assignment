using System;
using System.Collections.Generic;

#nullable disable

namespace Blogging.Data.Contract.Models
{
    public partial class PostTag
    {
        public long PostId { get; set; }
        public long Tagid { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
