using Microsoft.AspNetCore.Identity;
using ProStep.DataSourse.Context;
using ProStep.Model.Profile;
using ProStep.Model.Security;
using ProStep.SharedKernel.Enums.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataSourse.DataSeed
{
    public class SecuritySeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = (RoleManager<IdentityRole<Guid>>)services.GetService(typeof(RoleManager<IdentityRole<Guid>>));
            var userManager = (UserManager<User>)services.GetService(typeof(UserManager<User>));
            var context = (ProStepDBContext)services.GetService(typeof(ProStepDBContext));

            var newRoles = await CreateNewRoles(roleManager);
            await CreateSuperAdmin(context, userManager, roleManager);
            await CreateUser(context, userManager, roleManager);
            return;
        }

        private static async Task<IEnumerable<string>> CreateNewRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            var IdentityRoles = Enum.GetValues(typeof(ProStepRole)).Cast<ProStepRole>().Select(a => a.ToString());
            var ExistedRoles = roleManager.Roles.Select(a => a.Name).ToList();
            var newRoles = IdentityRoles.Except(ExistedRoles);

            foreach (var @new in newRoles)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>() { Name = @new });
            }

            return newRoles;
        }

        private static async Task CreateSuperAdmin(ProStepDBContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var superAdmin = await userManager.FindByNameAsync("caro");

            if (superAdmin is null)
            {
                superAdmin = new User
                {
                    FirstName = "Super Admin",
                    LastName = "",
                    UserName = "caro",
                    UserType = UserType.SuperAdmin,
                    PhoneNumber = "0930057225",
                    Email = "carolinedahi1212@gmail.com",
                    DateCreated = DateTime.UtcNow,
                    GenerationStamp = ""
                };

                var createResult = await userManager.CreateAsync(superAdmin, "1234");

                if (createResult.Succeeded && createResult.Succeeded)
                {
                    var identityRoles = roleManager.Roles.Select(a => a.Name).ToList();
                    var roleResult = await userManager.AddToRolesAsync(superAdmin, identityRoles);
                    if (roleResult.Succeeded)
                        return;
                    throw new Exception(string.Join("\n", roleResult.Errors.Select(error => error.Description)));
                }
                throw new Exception(string.Join("\n", createResult.Errors.Select(error => error.Description)));

            }
        }

        private static async Task CreateUser(ProStepDBContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var defaultUser = await userManager.FindByNameAsync("defaultUser");

            if (defaultUser is null)
            {
                defaultUser = new User
                {
                    FirstName = "Default",
                    LastName = "User",
                    UserName = "defaultUser",
                    UserType = UserType.SuperAdmin,
                    PhoneNumber = "0999999999",
                    Email = "defaultUser@gmail.com",
                    DateCreated = DateTime.UtcNow,
                    GenerationStamp = "",
                    Portfolio = new Portfolio
                    {
                        
                    }
                };

                var createResult = await userManager.CreateAsync(defaultUser, "1234");

                if (createResult.Succeeded && createResult.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(defaultUser, "User");
                    if (roleResult.Succeeded)
                        return;
                    throw new Exception(string.Join("\n", roleResult.Errors.Select(error => error.Description)));
                }
                throw new Exception(string.Join("\n", createResult.Errors.Select(error => error.Description)));
            }
        }
    }
}
