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
            _context.Add(category);
             return Save(); 
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Title).ToList();
        }

        public bool Save()
        {
            int isSaved = 0;
            try
            {
                isSaved = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return isSaved > 0;
            }
           
           return isSaved > 0;
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.CategoryId == categoryId);
        }
        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Where( c => c.CategoryId == categoryId).FirstOrDefault();
        }

        public ICollection<Post> GetPostsByCategoryId(int categoryId)
        {
            return _context.Posts.Where(c => c.CategoryId == categoryId).ToList();
        }

        public int IsUniqueTitle(string title)
        {
            return GetCategories().Count(c => c.Title.Trim().ToLower() == title.Trim().ToLower());
        }

    }
}
