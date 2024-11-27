using MNF_PORTAL_Core;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Infrastructure.Data
{
    public class IdentitySeeder
    {
        private readonly IUserService usersevice;
        private readonly IRoleService roleService;
        private readonly IUnitOfWork unitOfWork;

        public IdentitySeeder(IUserService usersevice,
            IRoleService roleService,
            IUnitOfWork unitOfWork)
        {
            this.usersevice = usersevice;
            this.roleService = roleService;
            this.unitOfWork = unitOfWork;
        }
        public async Task SeedAsync()
        {
            string RoleName = "SuperAdmin";
            if (!await unitOfWork.RoleRepository.RoleIsExistAsync(RoleName))
            {
                await roleService.AddRoleAsync(RoleName);
            }
            string username = "SuperAdmin_User";
            string password = "Admin_123";
            var user = await usersevice.GetUserByUserNameAsync(username);
            if (user == null) {
                ApplicationUser adminUser = new ApplicationUser()
                {
                    FullName = "_____Admin_____",
                    UserName = username,
                    Email = "Admin@gmail.com",
                    EmailConfirmed = true
                };
                var result = await usersevice.CreateUserAsync(adminUser, password);
                if (result.Succeeded)
                {
                    AddRoleToUserDTO model = new AddRoleToUserDTO()
                    {
                        UserId = adminUser.Id,
                        Roles = new List<string> { RoleName }
                    };
                    await usersevice.AddUserToRoleAsync(model);
                }
            }
            
        }
    }
}
