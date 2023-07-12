using AutoMapper;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogEngineWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryRepository
            , IPostRepository postRepository, IMapper mapper)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Loading categories and posts data");
            var categoryController = new CategoryController(_categoryRepository, _mapper);
            var categoryViewResult = (ViewResult)categoryController.GetCategories();

            var postController = new PostController(_postRepository, _mapper);
            var postViewResult = (ViewResult)postController.GetPosts();

            var listPost = postViewResult.ViewData.Model;
            var listCategory = categoryViewResult.ViewData.Model;

            var data = new List<object>() { listCategory, listPost};
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}