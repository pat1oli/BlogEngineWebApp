using BlogEngineWebApp.Data;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;

namespace BlogEngineWebApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateCategory(Category category)
        {
            try
            {
                _context.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }           
        }
        public bool UpdateCategory(int categoryId, Category category)
        {
            try
            {
                _context.Update(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.CategoryId == categoryId);
        }
        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Where( c => c.CategoryId == categoryId).First();
        }

        //TO DO: Move this method 
        public ICollection<Post> GetPostsByCategoryId(int categoryId)
        {
            return _context.Posts.Where(c => c.CategoryId == categoryId).ToList();
        }

    }
}
