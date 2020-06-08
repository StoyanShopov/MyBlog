using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Seeding
{
    using System.Linq;
    using System.Threading.Tasks;
    using Blog.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedUserAsync(userManager, GlobalConstants.AdministratorUserName, GlobalConstants.AdministratorPassword, GlobalConstants.AdministratorEmail);

            await SeedUserToRole(userManager, GlobalConstants.AdministratorUserName, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserToRole(UserManager<ApplicationUser> userManager, string username, string roleName)
        {
            var user = await userManager.FindByNameAsync(username);

            await userManager.AddToRoleAsync(user, roleName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username, string password, string email)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true,
                }, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
