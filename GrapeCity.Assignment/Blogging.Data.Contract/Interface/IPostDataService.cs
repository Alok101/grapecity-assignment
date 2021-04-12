using Blogging.Data.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogging.Data.Contract.Interface
{
    public interface IPostDataService
    {
        public IEnumerable<Post> GetPosts(Pagging pagging);
        public Post GetPost(long postId);
        public Task<long> CreatePost(Post post);
        public Task<SystemResponse> UpdatePost(Post updatePost);
        public Task<SystemResponse> RemovePost(long postId);
    }
}
