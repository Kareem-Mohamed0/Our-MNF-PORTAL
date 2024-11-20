using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Core.Interfaces_Repos
{
    public interface IRoleRepository
    {
        public Task<List<IdentityRole>> GetAllRolesAsync();
        public Task<IdentityRole> GetRoleByIdAsync(string RoleId);
        public Task<bool> AddRoleAsync(string role);
        public Task<bool> RemoveRoleAsync(string roleName);
        public Task<bool> UpdateRoleAsync(string OldRoleName, string NewRoleName);
        public Task<bool> RoleIsExistAsync(string RoleName);
    }
}
