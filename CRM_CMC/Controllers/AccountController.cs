using CRM_CMC.App_Models;
using CRM_CMC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CRM_CMC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepositories _accountRepositories;

        public AccountController(IAccountRepositories accountRepositories)
        {
            _accountRepositories = accountRepositories;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("Authorized");
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync(SignInModel model)
        {
            var token = await _accountRepositories.SignInAsync(model);

            if (token == null)
            {
                return Unauthorized("Invalid email or password");
            }
            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(SignUpModel model)
        {
            var result = await _accountRepositories.SignUpAsync(model);

            if (result.Succeeded)
            {
                return Ok("User created successfully");
            }

            return Unauthorized(result.Errors);
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpPost("seed-role")]
        public async Task<IActionResult> SeedRole()
        {
            var result = await _accountRepositories.SeedRole();

            if (result)
            {
                return Ok("Roles seeded successfully");
            }

            return BadRequest("Roles seeding failed");
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpGet("list-role")]
        public async Task<IActionResult> ListRole()
        {
            var result = await _accountRepositories.ListRole();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Roles not found");
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(string role)
        {
            var result = await _accountRepositories.AddRole(role);

            if (result)
            {
                return Ok("Role added successfully");
            }

            return BadRequest("Role already exists");
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpPost("add-role-claim")]
        public async Task<IActionResult> AddRoleClaim(string role, string permission)
        {
            var result = await _accountRepositories.AddRoleClaim(role, permission);

            if (result)
            {
                return Ok("Claim added successfully");
            }

            return BadRequest("Role or Claim not found");
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpPost("remove-role-claim")]
        public async Task<IActionResult> RemoveRoleClaim(string role, string permission)
        {
            var result = await _accountRepositories.RemoveRoleClaim(role, permission);

            if (result)
            {
                return Ok("Claim removed successfully");
            }

            return BadRequest("Role or Claim not found");
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpGet("get-role-claim")]
        public async Task<IActionResult> GetRoleClaim(string role)
        {
            var result = await _accountRepositories.GetRoleClaim(role);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Role not found");
        }


        [Authorize]
        [HttpGet("get-user-role")]
        public async Task<IActionResult> GetUserRole(Guid userID)
        {
            var result = await _accountRepositories.GetUserRole(userID);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("User not found");
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpPost("add-user-role")]
        public async Task<IActionResult> AddUserRole(Guid userID, string role)
        {
            var result = await _accountRepositories.AddUserToRole(userID, role);

            if (result.Item2)
            {
                return Ok(result.Item1);
            }

            return BadRequest(result.Item1);
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpPost("add-user-claim")]
        public async Task<IActionResult> AddUserClaim(Guid userID, string claim)
        {
            var result = await _accountRepositories.AddUserClaim(userID, claim);

            if (result)
            {
                return Ok("Claim added successfully");
            }

            return BadRequest("User not found");
        }

        [Authorize(Roles = StaticUserRole.Admin)]
        [HttpPost("remove-user-claim")]
        public async Task<IActionResult> RemoveUserClaim(Guid userID, string claim)
        {
            var result = await _accountRepositories.RemoveUserClaim(userID, claim);

            if (result)
            {
                return Ok("Claim removed successfully");
            }

            return BadRequest("User not found");
        }

        [Authorize]
        [HttpGet("get-user-claim")]
        public async Task<IActionResult> GetUserClaim(Guid userID)
        {
            var result = await _accountRepositories.GetUserClaim(userID);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("User not found");
        }



    }
}
