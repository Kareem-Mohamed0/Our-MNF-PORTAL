using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Service.DTOs;
using System.Security.Claims;

namespace MNF_PORTAL_Service.Interfaces
{
    public interface IRoleService
    {
        Task<List<DisplayRoleDTO>> GetAllRolesAsync();
        Task<DisplayRoleDTO> GetRoleByIdAsync(string RoleId);
        Task<bool> AddRoleAsync(string role);
        Task<bool> RemoveRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(UpdateRoleDTO model);
        Task<List<string>> GetclaimsAsync(string RoleName);
        Task<IdentityResult> RemoveClaimFromRoleAsync(IdentityRole Role, Claim claim);
    }
}
