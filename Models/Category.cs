using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineWebApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Title { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
