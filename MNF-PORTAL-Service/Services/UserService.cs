﻿using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;

namespace MNF_PORTAL_Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DetailsUserDTO> GetUserByIdAsync(string userId)
        {
            ApplicationUser userDB = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            IList<string> roles = await _unitOfWork.UserRepository.GetUserRolesAsync(userDB);
            var userDTO = new DetailsUserDTO
            {
                User_Id = userDB.Id,
                FirstName = userDB.FirstName,
                LastName = userDB.LastName,
                User_Name = userDB.UserName,
                Email = userDB.Email,
                Roles = roles,
                IsActive = userDB.IsActive
            };

            return userDTO;
        }

        public async Task<IEnumerable<DetailsUserDTO>> GetAllUsersAsync()
        {
            IEnumerable<ApplicationUser> usersDB = await _unitOfWork.UserRepository.GetAllUsersAsync();

            List<DetailsUserDTO> usersDTO = new List<DetailsUserDTO>();
            foreach (ApplicationUser user in usersDB)
            {
                IList<string> roles = await _unitOfWork.UserRepository.GetUserRolesAsync(user);
                var userDTO = new DetailsUserDTO
                {
                    User_Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    User_Name = user.UserName,
                    Email = user.Email,
                    Roles = roles,
                    IsActive = user.IsActive

                };
                usersDTO.Add(userDTO);
            }

            return usersDTO;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string Password)
        {
            IdentityResult result = await _unitOfWork.UserRepository.CreateUserAsync(user, Password);
            if (result.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                return result;
            }

            return result;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null) throw new ArgumentException("User not found.");
            if (await _unitOfWork.UserRepository.IsUserHaveRoleAsync(user, "SuperAdmin"))
            {
                throw new InvalidOperationException("SuperAdmin user cannot be deleted.");
            }
            await _unitOfWork.UserRepository.DeleteUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(string userId, DetailsUserDTO userDto)
        {

            // Find the user
            ApplicationUser user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return false;
            }
            // Update user properties
            user.Email = userDto.Email;
            user.UserName = userDto.User_Name;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.IsActive = userDto.IsActive;
            // Update user roles
            var userRoles = await _unitOfWork.UserRepository.GetUserRolesAsync(user);
            foreach (var role in userRoles)
            {
                await _unitOfWork.UserRepository.RemoveUserFromRoleAsync(user, role);
            }
            foreach (var role in userDto.Roles)
            {
                await _unitOfWork.UserRepository.AddUserToRoleAsync(user, role);
            }

            await _unitOfWork.UserRepository.UpdateUserAsync(user);

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(AddRoleToUserDTO model)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return IdentityResult.Failed();
            }
            foreach (var role in model.Roles)
            {
                var result = await _unitOfWork.UserRepository.AddUserToRoleAsync(user, role);
            }
            await _unitOfWork.CompleteAsync();
            return IdentityResult.Success;
        }

        public Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return _unitOfWork.UserRepository.CheckPasswordAsync(user, password);
        }

        public Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return _unitOfWork.UserRepository.GetUserRolesAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword)
        {
            if (user == null || string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(user), "User and new password cannot be null or empty.");
            if (!await _unitOfWork.UserRepository.UserIsExistsAsync(user))
                throw new ArgumentException("User does not exist.");
            var result = await _unitOfWork.UserRepository.RemoveUserPasswordAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException("Failed to remove user password.");

            result = await _unitOfWork.UserRepository.AddUserPasswordAsync(user, newPassword);

            return result;
        }
    }
}
