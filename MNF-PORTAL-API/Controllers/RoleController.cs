using Microsoft.AspNetCore.Mvc;
using MNF_PORTAL_Core.Constants;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;
using MNF_PORTAL_Service.Validators;
namespace MNF_PORTAL_API.Controllers
{

    [ApiController]
    [Route("API/Role")]
    //[Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService RoleService;

        public RoleController(IRoleService roleservice)
        {
            this.RoleService = roleservice;
        }
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] string RoleName)
        {
            try
            {

                var validator = new RoleValidator();
                var validationResult = await validator.ValidateAsync(RoleName);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var result = await RoleService.AddRoleAsync(RoleName);
                if (!result)
                    return BadRequest("Role Is Not Added");
                return Ok("Role Added  Successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            List<DisplayRoleDTO> Roles;
            try
            {
                Roles = await RoleService.GetAllRolesAsync();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(Roles);
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] string RoleName)
        {
            try
            {
                var result = await RoleService.RemoveRoleAsync(RoleName);
                if (!result)
                    return BadRequest("Role Is Not Deleted");
                return Ok("Role Deleted  Successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO model)
        {

            var validator = new RoleValidator();
            var validationResult = await validator.ValidateAsync(model.OldRole);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            validationResult = await validator.ValidateAsync(model.NewRole);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await RoleService.UpdateRoleAsync(model);
                if (!result)
                    return BadRequest("Role Is Not Updated");
                return Ok("Role Updated  Successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("GetAllPermissionsForRole/{RoleId}")]
        public async Task<IActionResult> GetAllPermissionsForRole(string RoleId)
        {
            var Role = await RoleService.GetRoleByIdAsync(RoleId);
            if (Role == null)
                return BadRequest("Role Not Found");

            var roleclaims = RoleService.GetclaimsAsync(Role.RoleName).Result;
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermessions = allClaims.Select(p => new PermissionDTO { permission = p }).ToList();
            foreach (var permission in allPermessions)
            {
                if (roleclaims.Any(c => c == permission.permission))
                {
                    permission.IsActive = true;
                }
                else
                    permission.IsActive = false;
            }

            var RoleWithPermessions = new PermissionOfRolesDTO
            {
                RoleId = RoleId,
                RoleName = Role.RoleName,
                AllPermissions = allPermessions
            };


            return Ok(RoleWithPermessions);



        }


        /*
        [HttpGet("UpdateAllPermissionsForRole/{RoleId}")]
        public async Task<IActionResult> UpdateAllPermissionsForRole(string RoleId , [FromBody] PermissionOfRolesDTO model)
        {
            var Role = await RoleService.GetRoleByIdAsync(RoleId);
            if (Role == null)
                return BadRequest("Role Not Found");

            var roleclaims = RoleService.GetclaimsAsync(Role.RoleName).Result;
            foreach (var claim in roleclaims)
            {
                await RoleService.RemoveClaimFromRoleAsync(nRole, claim);

            }









            var allClaims = Permissions.GenerateAllPermissions();
            model.AllPermissions = allClaims.Select(p => new PermissionDTO { permission = p }).ToList();
            foreach (var permission in allPermessions)
            {
                if (roleclaims.Any(c => c == permission.permission))
                {
                    permission.IsActive = true;
                }
                else
                    permission.IsActive = false;
            }

            var RoleWithPermessions = new PermissionOfRolesDTO
            {
                RoleId = RoleId,
                RoleName = Role.RoleName,
                AllPermissions = allPermessions
            };


            return Ok(RoleWithPermessions);



        }

        */


    }
}
