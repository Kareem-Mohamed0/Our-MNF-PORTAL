using Microsoft.AspNetCore.Mvc;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Core.Interfaces_Repos;
using MNF_PORTAL_Service.DTOs;
using MNF_PORTAL_Service.Interfaces;
using MNF_PORTAL_Service.Validators;

namespace MNF_PORTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService jwtService;

        public UserController(IUserService userService, IJwtService JwtService)
        {
            _userService = userService;
            jwtService = JwtService;
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
        // Get user by ID
        [HttpGet("Getbyusername/{username}")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var user = await _userService.GetUserByUserNameAsync(username);
            var userDTO = await _userService.GetUserByIdAsync(user.Id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(userDTO);
        }

        // Create a new user
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDTO model)
        {

            var validator = new UserValidator();
            var newuser = new DetailsUserDTO()
            {
                Full_Name = model.Full_Name,
                User_Name = model.User_Name,
                Email = model.Email

            };
            var validationResult = await validator.ValidateAsync(newuser);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }




            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                FullName = model.Full_Name,
                UserName = model.User_Name,
                Email = model.Email
            };
            var result = await _userService.CreateUserAsync(user, model.PassWord);
            if (model.Roles != null)
            {
                var UserWithRoles = new AddRoleToUserDTO
                {
                    UserId = user.Id,
                    Roles = model.Roles
                };
                await _userService.AddUserToRoleAsync(UserWithRoles);
            }
            if (result.Succeeded)
                return Ok("User created successfully.");

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserManager(string id, [FromBody] DetailsUserDTO userDto)
        {
            if (string.IsNullOrEmpty(id) || userDto == null)
            {
                return BadRequest("Invalid user ID or data.");
            }
            var validator = new UserValidator();
            var validationResult = await validator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }




            bool result = await _userService.UpdateUserAsync(id, userDto);
            if (result) { return Ok("User Updated successfully."); }

            return BadRequest("Invalid user ID or data.");
        }

        [HttpPut("UpdatebyUsername/{username}")]
        public async Task<IActionResult> UpdateUserManagerbyusername(string username, [FromBody] DetailsUserDTO userDto)
        {
            if (string.IsNullOrEmpty(username) || userDto == null)
            {
                return BadRequest("Invalid user ID or data.");
            }
            var validator = new UserValidator();
            var validationResult = await validator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _userService.GetUserByUserNameAsync(username);

            bool result = await _userService.UpdateUserAsync(user.Id, userDto);
            if (result) { return Ok("User Updated successfully."); }

            return BadRequest("Invalid Username or data.");
        }



        [HttpPost("AddRolesToUser")]
        public async Task<IActionResult> AddRolesToUser([FromBody] AddRoleToUserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _userService.AddUserToRoleAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Roles added to User successfully.");
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

        /*---------------------------------------- login ----------------------------------------*/
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _userService.GetUserByUserNameAsync(dto.Username);
            if (user == null || !await _userService.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid credentials.");
            if (!user.IsActive)
            {
                return Unauthorized("Your account is currently inactive. Please contact the administrator.");

            }

            var roles = await _userService.GetUserRolesAsync(user);
            var token = jwtService.GenerateToken(user, roles);

            return Ok(new { Token = token });
        }
        /*---------------------------------------- Reset Password ----------------------------------------*/
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            try
            {
                var user = await _userService.GetUserByUserNameAsync(model.Username);
                var result = await _userService.ChangePasswordAsync(user, model.NewPassword);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
                return Ok("Password reset successfully.");

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
