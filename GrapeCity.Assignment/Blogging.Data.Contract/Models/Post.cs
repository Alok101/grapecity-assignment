using System;
using System.Collections.Generic;

#nullable disable

namespace Blogging.Data.Contract.Models
{
    public partial class Post
    {
        public Post()
        {
            InverseParent = new HashSet<Post>();
            PostComments = new HashSet<PostComment>();
        }

        public long Id { get; set; }
        public long AuthorId { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string Content { get; set; }

        public virtual Post Parent { get; set; }
        public virtual ICollection<Post> InverseParent { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
    }
}
