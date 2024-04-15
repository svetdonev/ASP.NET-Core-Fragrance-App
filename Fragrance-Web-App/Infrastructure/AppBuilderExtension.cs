using Fragrance_Web_App.Data;
using Fragrance_Web_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Fragrance_Web_App.Infrastructure
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<FragranceAppDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(FragranceAppDbContext data)
        {
            if(data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Men's Perfumes"},
                new Category { Name = "Women's Perfumes"},
                new Category { Name = "Unisex Perfumes"},
            });

            data.SaveChanges();
        }
    }
}
