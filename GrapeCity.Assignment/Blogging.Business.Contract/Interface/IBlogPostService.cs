using Blogging.Business.Contract.Models;
using Blogging.Data.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogging.Business.Contract.Interface
{
    public interface IBlogPostService
    {
        public Post GetPost(long postId);
        public Task<long> CreatePost(PostViewModel post);
        public Task<SystemResponse> UpdatePost(PostViewModel updatePost);
        public Task<SystemResponse> RemovePost(long postId);
        public IEnumerable<Post> Posts(Pagging pagging);
    }
}
