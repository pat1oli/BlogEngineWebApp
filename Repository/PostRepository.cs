using BlogEngineWebApp.Data;
using BlogEngineWebApp.Models;
using BlogEngineWebApp.Repository.Interfaces;

namespace BlogEngineWebApp.Repository
{
    public class PostRepository : IPostRepository
    {

        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreatePost(Post post)
        {
            throw new NotImplementedException();
        }
        public bool UpdatePost(int postId, Post post)
        {
            throw new NotImplementedException();
        }

        public bool PostExists(int postId) {
            return _context.Posts.Any(p => p.PostId == postId);
        }

        public Post GetPostById(int postId)
        {
            return _context.Posts.Where(p => p.PostId == postId).First();
        }

        public ICollection<Post> GetPosts()
        {
            return _context.Posts.Where(p => p.PublicationDate <= DateTime.Now).OrderByDescending(p => p.PublicationDate).ToList();
        }
    }
}
