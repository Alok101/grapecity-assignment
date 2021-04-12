using Blogging.Business.Contract.Models;
using Blogging.Data.Contract.Models;

namespace Blogging.Business.Contract.ModelMapper
{
    public interface IModelBuilder
    {
        public Post BuildPostModel(PostViewModel post);
    }
}
