using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;

namespace BlogEngineWebApp.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        bool CreateCategory(Category category);    
        bool UpdateCategory(Category category);
        ICollection<Category> GetCategories();

        bool Save();
        bool CategoryExists(int categoryId);
        int IsUniqueTitle(string title);
        Category GetCategoryById(int categoryId);
        ICollection<Post> GetPostsByCategoryId(int categoryId);
    }
}
