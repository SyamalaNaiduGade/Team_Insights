using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TeamInsights
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var adminAlreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

            if (adminAlreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));

            var stdUserAlreadyExists = await roleManager.RoleExistsAsync(Constants.StandardUserRole);

            if (stdUserAlreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.StandardUserRole));
        }

        public static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "teaminsightsadmin@mail.com").SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new IdentityUser { UserName = "teaminsightsadmin@mail.com", Email = "teaminsightsadmin@mail.com" };

            await userManager.CreateAsync(testAdmin, "Admin@123");

            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
    
    }
}
