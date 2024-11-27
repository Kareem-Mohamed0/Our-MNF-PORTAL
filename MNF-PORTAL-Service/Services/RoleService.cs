using MNF_PORTAL_Core;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;

namespace MNF_PORTAL_Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /*=========================== Add Role ==============================*/
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
            var Role = await unitOfWork.RoleRepository.GetRoleByIdAsync(RoleId);
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
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Role name cannot be null or empty.");
            }
            if (!await unitOfWork.RoleRepository.RoleIsExistAsync(roleName))
            {
                throw new ArgumentException($"The role '{roleName}' does not exist.");
            }
            var result = await unitOfWork.RoleRepository.RemoveRoleAsync(roleName);
            if (!result)
            {
                throw new InvalidOperationException(message: "Failed to remove the role.");
            }
            return result;
        }
        /*=========================== update Role ==============================*/
        public async Task<bool> UpdateRoleAsync(UpdateRoleDTO model)
        {
            if (string.IsNullOrEmpty(model.NewRole) || string.IsNullOrEmpty(model.OldRole))
            {
                throw new ArgumentNullException(nameof(model.NewRole), "Role name cannot be null or empty.");
            }
            if (!await unitOfWork.RoleRepository.RoleIsExistAsync(model.OldRole))
            {
                throw new ArgumentException($"The role '{model.OldRole}' does not exist.");
            }
            var result = await unitOfWork.RoleRepository.UpdateRoleAsync(model.OldRole, model.NewRole);
            if (!result)
            {
                throw new InvalidOperationException(message: "Failed to update the role.");
            }
            //return await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
