﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;
using MNF_PORTAL_Service.Services;

namespace MNF_PORTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Get user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // Create a new user
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                FullName=model.Full_Name,
                UserName = model.User_Name,
                Email = model.Email
            };

            var result = await _userService.CreateUserAsync(user, model.PassWord);
            if (result.Succeeded)
                return Ok("User created successfully.");

            return BadRequest(result.Errors);
        }

        // Delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound("User not found.");

            return Ok("User deleted successfully.");
        }


    }
}
