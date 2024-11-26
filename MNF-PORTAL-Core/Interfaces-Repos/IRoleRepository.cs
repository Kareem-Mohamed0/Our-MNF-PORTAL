using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
        public Task<List<string>> GetclaimsAsync(string RoleName);
        public Task<IdentityResult> RemoveClaimFromRoleAsync(IdentityRole Role, Claim claim);
    }
}
