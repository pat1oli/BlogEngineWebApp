using BlogEngineWebApp.Data;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineWebApp.Helper
{
    public class UniqueTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var title = (string)value;

            // Check if any other records have the same title
            var isUnique = !dbContext.Categories.Any(c => c.Title.Trim().ToLower() == title.Trim().ToLower());

            if (!isUnique)
            {
                return new ValidationResult("The title must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}
