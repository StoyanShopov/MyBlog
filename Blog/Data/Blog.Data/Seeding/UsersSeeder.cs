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
            var roleManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(roleManager, GlobalConstants.AdministratorUserName, GlobalConstants.AdministratorPassword);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser
                {
                    UserName = username,
                    Email = username,
                }, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
