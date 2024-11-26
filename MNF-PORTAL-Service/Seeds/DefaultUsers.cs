using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core.Constants;
using MNF_PORTAL_Core.Entities;
using System.Security.Claims;

namespace MNF_PORTAL_Service.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedSuperAdminUserAsync(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            var defaultuser = new ApplicationUser
            {
                FullName = "SuperAdmin_________________________",
                UserName = "SuperAdmin",
                Email = "SuperAdmin@gmail.com"
            };
            var user = await usermanager.FindByEmailAsync(defaultuser.Email);
            if (user == null)
            {
                await usermanager.CreateAsync(defaultuser, "SuperAdmin@123");
                await usermanager.AddToRolesAsync(defaultuser, new List<string> { Roles.Basic.ToString(), Roles.Admin.ToString(), Roles.SuperAdmin.ToString() });
            }
            await rolemanager.SeedClaimsForSuperUser();
        }


        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> usermanager)
        {
            var defaultuser = new ApplicationUser
            {
                FullName = "Admin_________________________",
                UserName = "Admin",
                Email = "admin@gmail.com"
            };
            var user = await usermanager.FindByEmailAsync(defaultuser.Email);
            if (user == null)
            {
                await usermanager.CreateAsync(defaultuser, "Admin@123");
                await usermanager.AddToRoleAsync(defaultuser, Roles.Admin.ToString());
            }
        }

        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> usermanager)
        {
            var defaultuser = new ApplicationUser
            {
                FullName = "Basic_________________________",
                UserName = "Basic",
                Email = "Basic@gmail.com"
            };
            var user = await usermanager.FindByEmailAsync(defaultuser.Email);
            if (user == null)
            {
                await usermanager.CreateAsync(defaultuser, "Basic@123");
                await usermanager.AddToRoleAsync(defaultuser, Roles.Basic.ToString());
            }
        }

        private static async Task SeedClaimsForSuperUser(this RoleManager<IdentityRole> rolemanager)
        {
            var SuperAdmin = await rolemanager.FindByNameAsync(Roles.SuperAdmin.ToString());
            await rolemanager.AddPermissionClaims(SuperAdmin, Modules.Users.ToString());
            await rolemanager.AddPermissionClaims(SuperAdmin, Modules.Roles.ToString());

        }
        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> rolemanager, IdentityRole Role, string module)
        {
            var allclaims = await rolemanager.GetClaimsAsync(Role);
            var allPermissions = Permissions.GeneratePermissionsList(module);
            foreach (var permission in allPermissions)
            {
                if (!allclaims.Any(c => c.Type == "permission" && c.Value == permission))
                {
                    await rolemanager.AddClaimAsync(Role, new Claim("Permission", permission));
                }

            }
        }

    }
}