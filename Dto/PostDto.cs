using BlogEngineWebApp.Models;

namespace BlogEngineWebApp.Dto
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public CategoryDto Category { get; set; }
    }
}
