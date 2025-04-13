using TicketsCinema.Models;  // пространство имен модели User
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CustomIdentityApp
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminName = "admin";
            string password = "admin";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(adminName) == null)
            {
                User admin = new User { UserName = adminName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}