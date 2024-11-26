using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Service.DTOs;

namespace MNF_PORTAL_Service.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<DetailsUserDTO> GetUserByIdAsync(string userId);
        Task<IEnumerable<DetailsUserDTO>> GetAllUsersAsync();
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string Password);
        Task<IdentityResult> AddUserToRoleAsync(AddRoleToUserDTO model);
        Task<bool> UpdateUserAsync(string userId, DetailsUserDTO user);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword);

    }
}
