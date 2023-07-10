using BlogEngineWebApp.Models;

namespace BlogEngineWebApp.Repository.Interfaces
{
    public interface IPostRepository
    {
        bool CreatePost(Post post);
        bool UpdatePost(int postId, Post post);

        ICollection<Post> GetPosts { get; }
        Post GetPostById(int postId);
    }
}
