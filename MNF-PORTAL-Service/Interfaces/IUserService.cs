using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.Interfaces
{
    public interface IUserService
    {
        Task<DetailsUserDTO> GetUserByIdAsync(string userId);
        Task<IEnumerable<DetailsUserDTO>> GetAllUsersAsync();
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string Password);
        Task<bool> DeleteUserAsync(string userId);
    }
}
