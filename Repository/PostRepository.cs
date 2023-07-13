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
             _context.Add(post);
            return Save();
        }
        public bool UpdatePost(Post post)
        {
            _context.Update(post);
            return Save();
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
            return _context.Posts.Where(p => p.PublicationDate <= DateTime.Now).OrderBy(p => p.Title).ThenByDescending(p => p.PublicationDate).ToList();
        }

        public int IsUniqueTitle(string title)
        {
            return GetPosts().Count(x => x.Title.Trim().ToLower() == title.Trim().ToLower());
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
    }
}
