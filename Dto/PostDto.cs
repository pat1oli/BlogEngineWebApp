using BlogEngineWebApp.Helper;
using BlogEngineWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineWebApp.Dto
{
    public class PostDto
    {
        public int PostId { get; set; }

        [Display(Name = "Post title")]
        [Required(ErrorMessage = "Title is required")]
        [PostUniqueTitle]
        public string Title { get; set; }
        [Display(Name = "Post content")]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        [Display(Name = "Post publication date")]
        [DataType(DataType.DateTime, ErrorMessage = "Publication date is not valid")]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Post category")]
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
    }
}
