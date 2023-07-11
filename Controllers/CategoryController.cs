﻿using AutoMapper;
using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogEngineWebApp.Controllers
{

    [Route("/categories")]
    [ApiController]
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

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [SwaggerOperation(summary: "Add category", description: "Add a new category")]
        public IActionResult Add([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(ModelState);
            }

            bool isNotUnique = _categoryRepository.IsUniqueTitle(categoryDto.Title) > 0;
            if (isNotUnique)
            {
                ModelState.AddModelError("", "Category already exists!");
                return StatusCode(404, ModelState);
            }
           
            var categoryMap = _mapper.Map<Category>(categoryDto);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(summary: "Update category", description: "Update a title and return a status")]
        public IActionResult Update([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Can not be empty");
            }
            bool exists = _categoryRepository.CategoryExists(categoryDto.CategoryId);
            if(!exists) 
            {
                return BadRequest("How dare you");
            }

            bool moreTitle = _categoryRepository.IsUniqueTitle(categoryDto.Title) >= 2;
            if (moreTitle)
            {
                return BadRequest("Title already exists");
            }

            var category = _mapper.Map<Category>(categoryDto);
            if (!_categoryRepository.UpdateCategory(category))
            {
                return BadRequest("Database shut down");
            }          

            return Ok(category);
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(summary: "Get categories", description: "Return a list of categories")]
        public IActionResult GetCategories() { 
            var categories = _categoryRepository.GetCategories();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(categoriesDto.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(categories);
        }

        [HttpGet("categoryId")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(summary: "Get category with ID", description: "Return the category with [id]")]
        public IActionResult GetCategoryById(int id)
        {
            bool existsCategory = _categoryRepository.CategoryExists(id);
            if (!existsCategory)
            {
                return NotFound();
            }
            var category = _categoryRepository.GetCategoryById(id);
            var categoryDto = _mapper.Map <CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpGet("categoryId/posts")]
        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(summary: "Get posts with by categoryID", description: "Return all the posts of category [id]")]

        public IActionResult GetPostByCategoryId(int id)
        {
            bool existsCategory = _categoryRepository.CategoryExists(id);
            if (!existsCategory)
            {
                return NotFound();
            }
            var posts = _mapper.Map<List<PostDto>>(_categoryRepository.GetPostsByCategoryId(id));

            if (posts == null || posts.Count ==0)
                return NoContent();

            return Ok(posts);
        }
    }
}