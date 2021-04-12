using Blogging.Business.Contract.Models;
using Blogging.Data.Contract.Models;

namespace Blogging.Business.Contract.ModelMapper
{
    public class ModelBuilder : IModelBuilder
    {
        public Post BuildPostModel(PostViewModel post)
        {
            return new Post
            {
                Id = post.Id,
                AuthorId = post.AuthorId,
                ParentId = post.ParentId,
                Title = post.Title,
                MetaTitle = post.MetaTitle,
                Slug = post.Slug,
                Summary = post.Summary,
                Published = post.Published,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                PublishedAt = post.PublishedAt,
                Content = post.Content
            };
        }
    }
}
