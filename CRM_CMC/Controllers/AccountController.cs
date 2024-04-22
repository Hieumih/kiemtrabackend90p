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

        // [Authorize(Roles = $"{StaticUserRole.Admin}, {StaticUserRole.Staff}")]
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

        //[Authorize(Roles = StaticUserRole.Admin)]
        // [Authorize(Roles = "Admin")]
        [HttpPost("set-role")]
        public async Task<IActionResult> SetRole(Guid userID, string role)
        {
            var result = await _accountRepositories.SetRole(userID, role);

            if (result.Item2)
            {
                return Ok(result.Item1);
            }

            return BadRequest(result.Item1);
        }

        [Authorize]
        [HttpGet("get-role")]
        public async Task<IActionResult> GetRole(Guid userID)
        {
            var result = await _accountRepositories.GetRole(userID);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("User not found");
        }


    }
}
