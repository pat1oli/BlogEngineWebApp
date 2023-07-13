using BlogEngineWebApp.Models;

namespace BlogEngineWebApp.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //Categories
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>(){
                        new Category()
                        {
                            Title = "LifeStyle"
                        },
                        new Category()
                        {
                            Title = ".Net Core"
                        }
                    });
                    context.SaveChanges();
                }
                //Posts
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(new List<Post>(){
                        new Post()
                        {
                            Title = "Test Driven Development",
                            Content = "Lorem Ipsum TDD ",
                            PublicationDate = DateTime.Now,
                            Category= new Category{Title = "Best Practice"}
                        },
                        new Post()
                        {
                            Title = "Organize your room",
                            Content = "Best thing to do for improving and focusing on your goals ",
                            PublicationDate = DateTime.Now,
                            Category= new Category{Title = "Efficiency"}
                        }

                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
