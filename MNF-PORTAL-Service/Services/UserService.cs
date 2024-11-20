using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Core.Interfaces_Repos;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var userDTO = new DetailsUserDTO
            {
                User_Id = userDB.Id,
                Full_Name=userDB.FullName,
                User_Name=userDB.UserName,
                Email=userDB.Email
            
            };
            return userDTO;
        }

        public async Task<IEnumerable<DetailsUserDTO>> GetAllUsersAsync()
        {
            IEnumerable<ApplicationUser> usersDB = await _unitOfWork.UserRepository.GetAllUsersAsync();
            List<DetailsUserDTO> usersDTO = new List<DetailsUserDTO>();
            foreach (ApplicationUser user in usersDB) 
            {
                var userDTO = new DetailsUserDTO
                {
                    User_Id = user.Id,
                    Full_Name = user.FullName,
                    User_Name = user.UserName,
                    Email = user.Email

                };
                usersDTO.Add(userDTO);
            }
            return usersDTO;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user , string Password)
        {
            IdentityResult result = await _unitOfWork.UserRepository.CreateUserAsync(user, Password);
            if (result.Succeeded)
            {
                await _unitOfWork.SaveChangesAsync();
                return result;
            }
           
            return result;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            await _unitOfWork.UserRepository.DeleteUserAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
