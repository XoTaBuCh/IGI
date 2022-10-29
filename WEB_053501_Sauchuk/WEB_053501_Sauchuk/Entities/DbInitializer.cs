using Microsoft.AspNetCore.Identity;

namespace WEB_053501_Sauchuk.Entities;

public class DbInitializer
{
    public static async Task InitializeAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        string adminEmail = "aboba@gmail.com";
        string adminPassword = "aboba";

        if (await roleManager.FindByIdAsync("user") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("user"));
        }

        if (await roleManager.FindByIdAsync("admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }

        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            ApplicationUser admin = new ApplicationUser
                { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
            IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }

    public static async void Initialize(IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await DbInitializer.InitializeAsync(userManager, roleManager);
    }
}