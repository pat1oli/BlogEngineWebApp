using BlogEngineWebApp.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngineWebApp.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [UniqueTitle]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PublicationDate{ get; set; }

        [ForeignKey("CategoryId")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
