using AutoMapper;
using BlogEngineWebApp.Data;
using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace BlogEngineWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _appDbContext;


        public HomeController(ILogger<HomeController> logger, IMapper mapper
            , ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _appDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Loading categories and posts data");

            var categoryResult = _mapper.Map<List<CategoryDto>>(_appDbContext.Categories.ToList());
            var postResult = _mapper.Map<List<PostDto>>(_appDbContext.Posts.ToList());
            
            List<object> data = new List<object>() { categoryResult, postResult};
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}