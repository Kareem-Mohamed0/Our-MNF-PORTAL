using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Core.Interfaces_Repos;
using MNF_PORTAL_Infrastructure.Data;
using MNF_PORTAL_Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Infrastructure.Implementation_Repos
{
    public class UserRepository : GenericRepository<ApplicationUser>,IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager, AppDbContext context) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser user, string role)
        {
            // Remove the user from the role
            
            return await _userManager.RemoveFromRoleAsync(user, role);
        }
        




        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return   await _userManager.UpdateAsync(user);
        }



        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {

            // Delete the user
            return await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> RemoveUserPasswordAsync(ApplicationUser user)
        {
            return await _userManager.RemovePasswordAsync(user);
        }

        public async Task<IdentityResult> AddUserPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.AddPasswordAsync(user, password);
        }

        public async Task<bool> UserIsExistsAsync(ApplicationUser user)
        {
            return await _userManager.Users.AnyAsync(x => x.Id == user.Id);
        }
        public async Task<bool> IsUserHaveRoleAsync(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}
