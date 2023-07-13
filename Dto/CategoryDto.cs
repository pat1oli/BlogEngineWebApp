using System.ComponentModel.DataAnnotations;

namespace BlogEngineWebApp.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Title is required")]
        public string Title { get; set; }
    }
}
