using BlogEngineWebApp.Data;
using BlogEngineWebApp.Helper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineWebApp.Models
{
    [Index(nameof(Category.Title), IsUnique = true)]
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
