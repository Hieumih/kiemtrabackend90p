using CRM_CMC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




namespace CRM_CMC.Repositories
{
    public class AccountRepositories : IAccountRepositories
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly SignInManager<UserInfo> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepositories(UserManager<UserInfo> userManager, SignInManager<UserInfo> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken?> SignInAsync(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var userClaims = new List<Claim> // updated from new Claim to new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])); 

            // Create the JWT security token and encode it.
            var token = new JwtSecurityToken( 
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature)
            );
            
            return token;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new UserInfo
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
    }
}
