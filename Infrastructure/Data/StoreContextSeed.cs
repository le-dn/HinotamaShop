using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(
        StoreContext context,
        UserManager<AppUser> userManager,
        IConfiguration config
    )
    {
        var email = config["AdminUser:Email"]!;
        var password = config["AdminUser:Password"]!;

        if (!userManager.Users.Any(x => x.Email == email))
        {
            var user = new AppUser { UserName = email, Email = email };

            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Admin");
        }

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync(path + @"/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null)
                return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }

        if (!context.DeliveryMethods.Any())
        {
            var deliveryMethodsData = await File.ReadAllTextAsync(
                path + @"/Data/SeedData/delivery.json"
            );

            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(
                deliveryMethodsData
            );

            if (deliveryMethods == null)
                return;

            context.DeliveryMethods.AddRange(deliveryMethods);

            await context.SaveChangesAsync();
        }
    }
}
