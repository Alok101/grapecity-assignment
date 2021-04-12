using Blogging.Business.Contract.Interface;
using Blogging.Business.Contract.ModelMapper;
using Blogging.Business.Contract.Models;
using Blogging.Data.Contract.Interface;
using Blogging.Data.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogging.Business.Service.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IPostDataService _postDataService;
        private readonly IModelBuilder _modelBuilder;
        public BlogPostService(IPostDataService postDataService, IModelBuilder modelBuilder)
        {
            _postDataService = postDataService;
            _modelBuilder = modelBuilder;
        }
        public IEnumerable<Post> Posts(Pagging pagging)
        {
            return _postDataService.GetPosts(pagging);
        }
        public Post GetPost(long postId)
        {
            return _postDataService.GetPost(postId);
        }
        public Task<long> CreatePost(PostViewModel post)
        {
            var buildPostData = _modelBuilder.BuildPostModel(post);
            return _postDataService.CreatePost(buildPostData);
        }
        public Task<SystemResponse> UpdatePost(PostViewModel updatePost)
        {
            var buildPostData = _modelBuilder.BuildPostModel(updatePost);
            return _postDataService.UpdatePost(buildPostData);
        }
        public Task<SystemResponse> RemovePost(long postId)
        {
            return _postDataService.RemovePost(postId);
        }
    }
}
