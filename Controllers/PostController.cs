using AutoMapper;
using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;
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


        [HttpPost]
        [ProducesResponseType(typeof(PostDto), 200)]
        public IActionResult Add([FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest();
            }
            bool isNotUnique = _postRepository.IsUniqueTitle(postDto.Title)>=1;
            if (isNotUnique)
            {
                return BadRequest("Title must be unique");
            }
            var post = _mapper.Map<Post>(postDto);
            if (!_postRepository.CreatePost(post))
            {
                return BadRequest("Internal Server Problem");
            }
            return Ok(postDto);
        }
        [HttpPut]
        [ProducesResponseType(typeof(PostDto), 200)]
        public IActionResult UpdatePost([FromBody] PostDto postDto)
        {
            bool exists = _postRepository.PostExists(postDto.PostId);
            if (!exists)
            {
                return BadRequest("Post doesn't exist");
            }

            bool isTitleUnique = _postRepository.IsUniqueTitle(postDto.Title) >= 1;
            if (!isTitleUnique)
            {
                return BadRequest("Title already exists.");
            }

            var post = _mapper.Map<Post>(postDto);
            if (!_postRepository.UpdatePost(post))
            {
                return BadRequest();
            }

            return View("Edit", postDto);
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult GetPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(_postRepository.GetPosts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (posts == null || posts.Count == 0)
                return NoContent();

            return View("Posts", posts);
        }

        [HttpGet("/postId")]
        [ProducesResponseType(typeof(PostDto), 200)]
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