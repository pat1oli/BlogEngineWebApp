using BlogEngineWebApp.Data;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineWebApp.Helper
{
    public class PostUniqueTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var title = (string)value;
            var isUnique = !dbContext.Posts.Any(c => c.Title.Trim().ToLower() == title.Trim().ToLower());

            if (!isUnique)
            {
                return new ValidationResult("The title must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}
