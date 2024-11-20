using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.Interfaces
{
    public interface IRoleService
    {
        public Task<List<DisplayRoleDTO>> GetAllRolesAsync();
        public Task<DisplayRoleDTO> GetRoleByIdAsync(string RoleId);
        public Task<bool> AddRoleAsync(string role);
        public Task<bool> RemoveRoleAsync(string roleName);
        public Task<bool> updateRoleAsync(string OldRoleName, string NewRoleName);
    }
}
