using Blogging.Business.Contract.Interface;
using Blogging.Business.Contract.Models;
using Blogging.Data.Contract.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogging.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BlogPostController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;
        private readonly IBlogPostService _blogService;
        public BlogPostController(IBlogPostService blogService, ILogger<BlogPostController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }
        [HttpGet, Route("posts")]
        public IActionResult Get(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_blogService.Posts(new PageViewModel { CurrentPage = pageNumber, PageSize = pageSize }));
        }
        [HttpGet("{postId}", Name = "GetPost")]
        public IActionResult Get(long postId)
        {
            var post = _blogService.GetPost(postId);
            if (post == null)
                return NotFound();
            return Ok(post);
        }
        [HttpPost, Route("post")]
        public IActionResult CreatePost([FromBody] PostViewModel post)
        {
            if (post == null)
                return BadRequest("InValid Request");
            var newPostId = _blogService.CreatePost(post);
            return CreatedAtRoute("GetPost", new { postId = newPostId.Id }, post);
        }
        [HttpPut, Route("post")]
        public IActionResult UpdatePost([FromBody] PostViewModel post)
        {
            if (post == null)
                return BadRequest("InValid Request");
            return Ok(_blogService.UpdatePost(post));
        }
        [HttpDelete, Route("post/{postId}")]
        public IActionResult RemovePost(long postId)
        {
            var resultData = _blogService.RemovePost(postId);
            if (resultData.Result.Response == ResponseType.success)
                return NoContent();
            return NotFound();
        }
    }
}
