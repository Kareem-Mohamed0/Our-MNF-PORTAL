﻿using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Core.Interfaces_Repos
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<bool>CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> RemoveUserPasswordAsync(ApplicationUser user);
        Task<IdentityResult> AddUserPasswordAsync(ApplicationUser user, string password);
        Task<bool> UserIsExistsAsync(ApplicationUser user);
        public Task<bool> IsUserHaveRoleAsync(ApplicationUser user, string roleName);

    }
}
