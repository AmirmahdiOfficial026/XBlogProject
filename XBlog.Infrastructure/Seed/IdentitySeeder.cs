using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using XBlog.Infrastructure.Identity;

namespace XBlog.Infrastructure.Seed;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider service)
    {
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        if (userManager.Users.Count() == 0)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var adminUser = await userManager.FindByNameAsync("Admin");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                };
                var reuslt = await userManager.CreateAsync(adminUser, "Admin@123");
                if (reuslt.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
