﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MNF_PORTAL_Core.Interfaces_Repos;
using MNF_PORTAL_Infrastructure.Data;
using MNF_PORTAL_Infrastructure.Implementation_Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager,AppDbContext context) : base(context)
        {
            
            this.roleManager = roleManager;
        }

        public async Task<bool> AddRoleAsync(string role)
        {
            var Role = new IdentityRole { Name = role };
            var result = await roleManager.CreateAsync(Role);
            return result.Succeeded;
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var result = await roleManager.Roles.ToListAsync();
            return result;
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string RoleId)
        {
            var result = await roleManager.FindByIdAsync(RoleId);
            return result;
        }

        public async Task<bool> RemoveRoleAsync(string roleName)
        {
            var Role = await roleManager.FindByNameAsync(roleName);
            var result = await roleManager.DeleteAsync(Role);
            return result.Succeeded;
        }

        public async Task<bool> RoleIsExistAsync(string roleName)
        {
                var role = await roleManager.FindByNameAsync(roleName);
                return role != null;
            // Returns true if role is found, false if not
        }


        public async Task<bool> UpdateRoleAsync(string OldRoleName, string NewRoleName)
        {
            var Role = await roleManager.FindByNameAsync(OldRoleName);
            if (Role == null) return false;
            Role.Name = NewRoleName;
            Role.NormalizedName = NewRoleName.ToUpper();
            var result =await roleManager.UpdateAsync(Role);
            return true;
        }
    }
}
