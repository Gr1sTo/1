using Microsoft.AspNetCore.Identity;

public class RoleInitializer
{
    public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string adminEmail = "admin@example.com";
        string password = "Admin123!";

        // Видалення ролей (опційно, якщо потрібно очистити)
        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole != null)
        {
            await roleManager.DeleteAsync(adminRole);
            Console.WriteLine("Existing 'Admin' role deleted.");
        }

        // Створення ролі Admin
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            Console.WriteLine("Role 'Admin' created.");
        }

        // Створення користувача
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
            var result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
                Console.WriteLine($"Admin user '{adminEmail}' created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create admin user:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine("Admin user already exists.");
        }
    }
}
