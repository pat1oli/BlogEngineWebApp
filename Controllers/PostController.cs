using AutoMapper;
using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineWebApp.Controllers
{
    [Produces("application/json")]
    public class PostController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Category> categories = _categoryRepository.GetCategories().ToList();
            ViewBag.CategoriesEnum = new SelectList(categories, "CategoryId", "Title");
            return View("Create");
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostDto), 200)]
        public IActionResult Add(PostDto postDto)
        {
            if (postDto == null)
            {
                return View("NotFound");
            }
            bool isNotUnique = _postRepository.IsUniqueTitle(postDto.Title) >= 1;
            if (isNotUnique)
            {
                return BadRequest("Title must be unique");
            }
            var post = _mapper.Map<Post>(postDto);
            if (!_postRepository.CreatePost(post))
            {
                return BadRequest("Something is missing, surely category");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/post/update/{postId}")]
        public IActionResult Update(int postId) {
            List<Category> categories = _categoryRepository.GetCategories().ToList();
            ViewBag.CategoriesEnum = new SelectList(categories, "CategoryId", "Title");

            var post = _postRepository.GetPostById(postId);
            if (post == null)
            {
                return View("NotFound");
            }
            var postDto = _mapper.Map<PostDto>(post);
            return View("Edit", postDto);

        }

        [HttpPost("/post/update/{postId}")]
        [ProducesResponseType(typeof(PostDto), 200)]
        public IActionResult Update(int postId, PostDto postDto)
        {
            bool exists = _postRepository.PostExists(postDto.PostId);
            if (!exists)
            {
                return View("NotFound");
            }

            bool isTitleUnique = _postRepository.IsUniqueTitle(postDto.Title) <= 1;
            if (!isTitleUnique)
            {
                return BadRequest("Title already exists.");
            }

            var post = _mapper.Map<Post>(postDto);
            if (!_postRepository.UpdatePost(post))
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet("/posts")]
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

        [HttpGet("/posts/{postId}")]
        [ProducesResponseType(typeof(PostDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPostById(int postId)
        {
            var result = _postRepository.PostExists(postId);
            if (!result)
            {
                return NotFound();
            }
            var post = _mapper.Map<PostDto>(_postRepository.GetPostById(postId));

            return Ok(post);
        }
    }
}