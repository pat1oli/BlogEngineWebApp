using BlogEngineWebApp.Models;
using System.Collections.ObjectModel;

namespace BlogEngineWebApp.Repository.Interfaces
{
    public interface IPostRepository
    {
        bool CreatePost(Post post);
        bool UpdatePost(Post post);

        bool PostExists(int postId);

        ICollection<Post> GetPosts();
        Post GetPostById(int postId);
        int IsUniqueTitle(string title);
    }
}
