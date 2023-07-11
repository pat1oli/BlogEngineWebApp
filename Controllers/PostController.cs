using AutoMapper;
using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository;
using BlogEngineWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngineWebApp.Controllers
{
    [Route("/posts")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult GetPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(_postRepository.GetPosts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (posts == null || posts.Count == 0)
                return NoContent();

            return Ok(posts);
        }

        [HttpGet("/postId")]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPostById(int id)
        {
            var result = _postRepository.PostExists(id);
            if (!result)
            {
                return NotFound();
            }
            var post = _mapper.Map<PostDto>(_postRepository.GetPostById(id));

            return Ok(post);
        }
    }
}
