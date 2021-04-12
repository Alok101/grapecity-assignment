using Blogging.Data.Contract.CustomException;
using Blogging.Data.Contract.Data;
using Blogging.Data.Contract.Interface;
using Blogging.Data.Contract.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Blogging.Data.Service.Services
{
    public class PostDataService : IPostDataService
    {
        private readonly blogContext _dataContext;
        private readonly ILogger _logger;
        public PostDataService(blogContext dataContext, ILogger logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        public IEnumerable<Post> GetPosts(Pagging pagging)
        {
            IEnumerable<Post> requiredPosts = null;
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
            requiredPosts = _dataContext.Set<Post>()
                .AsNoTracking()
              .Skip((pagging.CurrentPage - 1) * pagging.PageSize).Take(pagging.PageSize).ToList();
            scope.Complete();
            return requiredPosts;
        }
        public Post GetPost(long postId)
        {
            return _dataContext.Set<Post>()
                  .AsNoTracking()
                  .SingleOrDefault(p => p.Id == postId);
        }
        public async Task<long> CreatePost(Post post)
        {
            _dataContext.Set<Post>().Add(post);
            await _dataContext.SaveChangesAsync();
            return post.Id;
        }
        public async Task<SystemResponse> UpdatePost(Post updatePost)
        {
            Post postObject = _dataContext.Set<Post>()
                  .AsTracking()
                  .FirstOrDefault(p => p.Id == updatePost.Id);
            try
            {
                if (postObject == null)
                {
                    throw new RecordNotFoundException($"Post is not found {updatePost.Id}");
                }
                _dataContext.Entry(postObject).CurrentValues.SetValues(updatePost);
                await _dataContext.SaveChangesAsync();
                return SystemResponse.SuccessResponse("Success");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Update post failed");
                _dataContext.Entry(postObject).Reload();
                throw new UpdateFailedException("Update post failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update post failed");
                throw new UpdateFailedException("Update post failed");
            }
        }
        public async Task<SystemResponse> RemovePost(long postId)
        {
            Post postObject = _dataContext.Set<Post>()
                  .AsTracking()
                  .FirstOrDefault(p => p.Id == postId);
            try
            {
                if (postObject == null)
                {
                    throw new RecordNotFoundException($"Post is not found {postId}");
                }
                _dataContext.Entry(postObject).State = EntityState.Deleted;
                await _dataContext.SaveChangesAsync();
                return SystemResponse.SuccessResponse("Success");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Update post failed");
                _dataContext.Entry(postObject).Reload();
                throw new UpdateFailedException("Update post failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update post failed");
                throw new UpdateFailedException("Update post failed");
            }
        }

    }
}
