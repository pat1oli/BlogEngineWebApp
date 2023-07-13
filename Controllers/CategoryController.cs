using AutoMapper;
using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogEngineWebApp.Controllers
{
    [Produces("application/json")]
    [SwaggerTag("Category", "Endpoints for category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Create");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [SwaggerOperation(summary: "Add category", description: "Add a new category")]
        public IActionResult Add(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View("NotFound",categoryDto);
            }

            bool isNotUnique = _categoryRepository.IsUniqueTitle(categoryDto.Title) > 0;
            if (isNotUnique)
            {
                ModelState.AddModelError("", "Category already exists!");
                return NotFound(ModelState);
            }
           
            var categoryMap = _mapper.Map<Category>(categoryDto);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("category/update/{categoryId}")]
        public IActionResult Update(int categoryId)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                return View("NotFound");
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return View("Edit", categoryDto);
        }

        [HttpPost]
        [Route("category/update/{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(summary: "Update category", description: "Update a title and return a status")]
        public IActionResult Update(int categoryId, CategoryDto categoryDto)
        {
            bool IsNotUnique = _categoryRepository.IsUniqueTitle(categoryDto.Title) > 1;
            if (IsNotUnique)
            {
                ModelState.AddModelError("Title", "Category title must be unique");
                return View(categoryDto);
            }
            var newCategory = new Category()
            {
                CategoryId = categoryId,
                Title = categoryDto.Title
            };

            if (!_categoryRepository.UpdateCategory(newCategory))
            {
                return View("Error");
            }          

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("/categories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(summary: "Get categories", description: "Return a list of categories")]
        public IActionResult GetCategories() { 
            var categories = _categoryRepository.GetCategories();
            if (categories.IsNullOrEmpty())
            {
                return NoContent();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            
            return View("Categories", categoriesDto);
        }

        [HttpGet]
        [Route("/categories/{categoryId}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(summary: "Get category with ID", description: "Return the category with [id]")]
        public IActionResult GetCategoryById(int categoryId)
        {
            bool existsCategory = _categoryRepository.CategoryExists(categoryId);
            if (!existsCategory)
            {
                return NotFound();
            }
            var category = _categoryRepository.GetCategoryById(categoryId);
            var categoryDto = _mapper.Map <CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpGet("/categories/{categoryId}/posts")]
        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(summary: "Get posts with by categoryID", description: "Return all the posts of category [id]")]
        public IActionResult GetPostByCategoryId(int categoryId)
        {
            bool existsCategory = _categoryRepository.CategoryExists(categoryId);
            if (!existsCategory)
            {
                return NotFound();
            }
            var posts = _categoryRepository.GetPostsByCategoryId(categoryId);
            var postsDto = _mapper.Map<List<PostDto>>(posts);

            if (postsDto == null || postsDto.Count == 0)
            {
                ModelState.AddModelError("Error", "No Post with this Category!");
                return NoContent();
            }
                

            return Ok(posts);
        }
    }
}
