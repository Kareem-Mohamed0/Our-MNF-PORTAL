using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /*=========================== Add ==============================*/
        public async Task<bool> AddRoleAsync(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException(nameof(role), "Role name cannot be null or empty.");
            }

            //// Check if the role already exists
            if (await unitOfWork.RoleRepository.RoleIsExistAsync(role))
            {
                throw new InvalidOperationException($"The role '{role}' already exists.");
            }

            // Try to add the new role
            var result = await unitOfWork.RoleRepository.AddRoleAsync(role);

            //If the role addition failed, throw an exception or handle accordingly
            if (!result)
            {
                throw new InvalidOperationException(message: "Failed to add the role.");
            }
            // Return true indicating the role was successfully added
            return result;
        }

        /*=========================== Get All Roles ==============================*/
        public async Task<List<DisplayRoleDTO>> GetAllRolesAsync()
        {
            var roles = await unitOfWork.RoleRepository.GetAllRolesAsync();
            var result = roles.Select(r => new DisplayRoleDTO
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                }).ToList();
            return result;
        }
        /*=========================== Get Role By Id ==============================*/
        public async Task<DisplayRoleDTO> GetRoleByIdAsync(string RoleId)
        {
            var Role =await unitOfWork.RoleRepository.GetRoleByIdAsync(RoleId);
            var result = new DisplayRoleDTO()
            {
                RoleId = Role.Id,
                RoleName = Role.Name
            };
            return result;
        }
        /*=========================== Remove Role By Name ==============================*/
        public async Task<bool> RemoveRoleAsync(string roleName)
        {
            await unitOfWork.RoleRepository.RemoveRoleAsync(roleName);
            return await unitOfWork.CompleteAsync();
        }
        /*=========================== update Role ==============================*/
        public async Task<bool> updateRoleAsync(string OldRoleName, string NewRoleName)
        {
            if (string.IsNullOrEmpty(NewRoleName))
            {
                throw new ArgumentNullException(nameof(NewRoleName), "Role name cannot be null or empty.");
            }
            if(string.IsNullOrEmpty(OldRoleName))
            {
                throw new ArgumentNullException(nameof(OldRoleName), "Role name cannot be null or empty.");
            }
            if(!await unitOfWork.RoleRepository.RoleIsExistAsync(OldRoleName))
                throw new ArgumentException($"The role '{OldRoleName}' does not exist.");
            return await unitOfWork.RoleRepository.UpdateRoleAsync(OldRoleName, NewRoleName);
        }
    }
}
