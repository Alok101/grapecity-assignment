using System;
using System.Collections.Generic;

#nullable disable

namespace Blogging.Data.Contract.Models
{
    public partial class Tag
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
    }
}
