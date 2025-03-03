using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using File = System.IO.File;

namespace Ecommerce.Infrastructure.Persistences;

public class EcommerceDbContextData
{
    public static async Task LoadData(
        EcommerceDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            if (!userManager.Users.Any())
            {
                var userAdmin = new User
                {
                    FirstName = "John",
                    LastName = "Arevalo",
                    Email = "john.arevalo@yopmail.com",
                    UserName = "john.arevalo",
                    Phone = "3135359237",
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/vaxidrez.jpg?alt=media&token=14a28860-d149-461e-9c25-9774d7ac1b24",
                };
                
                await userManager.CreateAsync(userAdmin, "John123$");
                await userManager.AddToRoleAsync(userAdmin, Role.ADMIN);
                  
                var userDefault = new User
                {
                    FirstName = "Eliana",
                    LastName = "Sanchez",
                    Email = "eliana.sanchez@yopmail.com",
                    UserName = "eliana.sanchez",
                    Phone = "3135359238",
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d",
                };
                
                await userManager.CreateAsync(userDefault, "Eliana123$");
                await userManager.AddToRoleAsync(userDefault, Role.USER);
            }

            if (!context.Categories!.Any())
            {
                var data = await File.ReadAllTextAsync("../../Infrastructure/Ecommerce.Infrastructure/Data/category.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(data);
                await context.Categories!.AddRangeAsync(categories!);
                await context.SaveChangesAsync();
            }
            
            if (!context.Products!.Any())
            {
                var data = await File.ReadAllTextAsync("../../Infrastructure/Ecommerce.Infrastructure/Data/product.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(data);
                await context.Products!.AddRangeAsync(products!);
                await context.SaveChangesAsync();
            }
            
            if (!context.Images!.Any())
            {
                var data = await File.ReadAllTextAsync("../../Infrastructure/Ecommerce.Infrastructure/Data/image.json");
                var images = JsonConvert.DeserializeObject<List<Image>>(data);
                await context.Images!.AddRangeAsync(images!);
                await context.SaveChangesAsync();
            }
            
            if (!context.Reviews!.Any())
            {
                var data = await File.ReadAllTextAsync("../../Infrastructure/Ecommerce.Infrastructure/Data/review.json");
                var reviews = JsonConvert.DeserializeObject<List<Review>>(data);
                await context.Reviews!.AddRangeAsync(reviews!);
                await context.SaveChangesAsync();
            }

            if (!context.Countries!.Any())
            {
                var data = await File.ReadAllTextAsync("../../Infrastructure/Ecommerce.Infrastructure/Data/countries.json");
                var countries = JsonConvert.DeserializeObject<List<Country>>(data);
                await context.Countries!.AddRangeAsync(countries!);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
            logger.LogError(e.Message);
        }
    }
}