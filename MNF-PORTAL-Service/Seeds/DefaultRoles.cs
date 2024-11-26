using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core.Constants;

namespace MNF_PORTAL_Service.Seeds
{
    public static class DefaultRoles
    {

        public static async Task SeedAsync(RoleManager<IdentityRole> rolemanager)
        {
            await rolemanager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await rolemanager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await rolemanager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}
