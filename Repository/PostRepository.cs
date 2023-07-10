using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;

namespace BlogEngineWebApp.Repository
{
    public class PostRepository : IPostRepository
    {
        public ICollection<Post> GetPosts => throw new NotImplementedException();

        public bool CreatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(int postId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(int postId, Post post)
        {
            throw new NotImplementedException();
        }
    }
}
