using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository;
using BlogEngineWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngineWebApp.Controllers
{

    [Route("/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        [HttpPost]
        public IActionResult Add(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }
            var result = _categoryRepository.CreateCategory(category);
            if (result)
            {
                TempData["msg"] = "Category Added Successfully!";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side!";

            return View(category);
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult GetCategories() { 
            var categories = _categoryRepository.GetCategories();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(categories == null || categories.Count == 0)
            {
                return NoContent();
            }
            return Ok(categories);
        }

        [HttpGet("categoryId")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetCategoryById(int id)
        {
            bool existsCategory = _categoryRepository.CategoryExists(id);
            if (!existsCategory)
            {
                return NotFound();
            }
            var category = _categoryRepository.GetCategoryById(id);
            return Ok(category);
        }

        [HttpGet("categoryId/posts")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult GetPostByCategoryId(int id)
        {
            bool existsCategory = _categoryRepository.CategoryExists(id);
            if (!existsCategory)
            {
                return NotFound();
            }
            var posts = _categoryRepository.GetPostsByCategoryId(id);
            if (posts == null || posts.Count ==0)
                return NoContent();

            return Ok(posts);
        }
    }
}
