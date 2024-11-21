
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;
namespace MNF_PORTAL_API.Controllers
{

    [ApiController]
    [Route("API/Role")]
    [Authorize(Roles = "Admin")]
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
                var result = await RoleService.AddRoleAsync(RoleName);
                if(!result)
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
            catch(ArgumentException ex)
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
        public async Task<IActionResult> UpdateRole([FromBody]UpdateRoleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await RoleService.UpdateRoleAsync( model);
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
    }
}
