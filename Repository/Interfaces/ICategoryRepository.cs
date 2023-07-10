using BlogEngineWebApp.Models;

namespace BlogEngineWebApp.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        bool CreateCategory(Category category);    
        bool UpdateCategory(int categoryId, Category category);
        ICollection<Category> GetCategories();
        bool CategoryExists(int categoryId);
        Category GetCategoryById(int categoryId);
        ICollection<Post> GetPostsByCategoryId(int categoryId);
    }
}
