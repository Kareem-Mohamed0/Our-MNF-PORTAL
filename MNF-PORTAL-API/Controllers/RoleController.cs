﻿using Microsoft.AspNetCore.Mvc;
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


        /*=========================== Add Role ==============================*/
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

        /*=========================== Get All Roles ==============================*/
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

        /*=========================== Update Role ==============================*/
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

        /*=========================== Delete Role ==============================*/
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
    }
}
