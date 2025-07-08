using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceMini.Data
{
    public static class DbInitializer
    {
        private const string AdminEmail = "admin@shop.com";
        private const string AdminPassword = "Admin@123";

        private const string CustomerEmail = "customer@shop.com";
        private const string CustomerPassword = "Customer@123";

        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            
            var roles = new[] { "Admin", "Customer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            
            var adminUser = await userManager.FindByEmailAsync(AdminEmail);
            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, AdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            
            var customerUser = await userManager.FindByEmailAsync(CustomerEmail);
            if (customerUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = CustomerEmail,
                    Email = CustomerEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, CustomerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }
        }
    }
}
