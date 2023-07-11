using BlogEngineWebApp.Data;
using BlogEngineWebApp.Helper;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineWebApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        
        [Required]
        [UniqueTitle]
        public string Title { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
