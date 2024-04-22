using CRM_CMC.App_Models;
using CRM_CMC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;




namespace CRM_CMC.Repositories
{
    public class AccountRepositories : IAccountRepositories
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly SignInManager<UserInfo> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountRepositories(UserManager<UserInfo> userManager, SignInManager<UserInfo> signInManager, IConfiguration configuration, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<JwtSecurityToken?> SignInAsync(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return null;
            }





            var userClaims = new List<Claim> // updated from new Claim to new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // find the role of the user
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

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
            // check if the user already exists
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User already exists" });
            }

            var user = new UserInfo
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return result;
            }

            // add the user to the customer role
            await _userManager.AddToRoleAsync(user, StaticUserRole.Customer);

            return result;
        }
        // role
        public async Task<bool> SeedRole()
        {
            bool isAdminExists = await _roleManager.RoleExistsAsync(StaticUserRole.Admin);
            bool isStaffExists = await _roleManager.RoleExistsAsync(StaticUserRole.Staff);
            bool isCustomerExists = await _roleManager.RoleExistsAsync(StaticUserRole.Customer);

            if (!isAdminExists)
            {
                await _roleManager.CreateAsync(new UserRole(StaticUserRole.Admin));
            }

            if (!isStaffExists)
            {
                await _roleManager.CreateAsync(new UserRole(StaticUserRole.Staff));
            }

            if (!isCustomerExists)
            {
                await _roleManager.CreateAsync(new UserRole(StaticUserRole.Customer));
            }

            return true;
        }
        public async Task<IEnumerable<string>> ListRole()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return roles;
        }
        public async Task<bool> AddRole(string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (roleExists)
            {
                return false;
            }

            await _roleManager.CreateAsync(new UserRole(role));
            return true;
        }
        public async Task<bool> AddRoleClaim(string role, string claim)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return false;
            }
            // check if the permission exists in StaticRoleClaim
            var claimExists = typeof(StaticRoleClaim).GetFields().Any(f => f.GetValue(null).ToString() == claim);
            if (!claimExists)
            {
                return false;
            }
            var permissionClaim = new Claim(ClaimTypes.Role, claim);
            var roleObj = await _roleManager.FindByNameAsync(role);
            await _roleManager.AddClaimAsync(roleObj, permissionClaim);

            return true;
        }
        public async Task<bool> RemoveRoleClaim(string role, string claim)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return false;
            }
            // check if the permission exists in StaticRoleClaim
            var claimExists = typeof(StaticRoleClaim).GetFields().Any(f => f.GetValue(null).ToString() == claim);
            if (!claimExists)
            {
                return false;
            }
            var permissionClaim = new Claim(ClaimTypes.Role, claim);
            var roleObj = await _roleManager.FindByNameAsync(role);
            await _roleManager.RemoveClaimAsync(roleObj, permissionClaim);

            return true;
        }

        public async Task<IEnumerable<string>?> GetRoleClaim(string role)
        {
            // get role
            var roleObj = await _roleManager.FindByNameAsync(role);
            if (roleObj == null)
            {
                return null;
            }
            // get cliams
            var claims = await _roleManager.GetClaimsAsync(roleObj);
            var claimValues = claims.Select(c => c.Value);
            return claimValues;
        }

        // user
        public async Task<IEnumerable<string>?> GetUserRole(Guid userID)
        {
            var user = await _userManager.FindByIdAsync(userID.ToString());
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }
        public async Task<(string, bool)> AddUserToRole(Guid userID, string role)
        {
            // find the user
            var user = await _userManager.FindByIdAsync(userID.ToString());
            if (user == null)
            {
                return ("user not found", false);
            }
            // check if the role exists
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return ("Role not found", false);
            }
            await _userManager.AddToRoleAsync(user, role);
            return ($"set role of {userID} to {role}", true);

        }
        public async Task<bool> AddUserClaim(Guid userID, string claim)
        {
            var user = await _userManager.FindByIdAsync(userID.ToString());
            if (user == null)
            {
                return false;
            }

            var claimExists = typeof(StaticRoleClaim).GetFields().Any(f => f.GetValue(null).ToString() == claim);
            if (!claimExists)
            {
                return false;
            }

            var claimObj = new Claim(ClaimTypes.Role, claim);
            await _userManager.AddClaimAsync(user, claimObj);

            return true;
        }

        public async Task<bool> RemoveUserClaim(Guid userID, string claim)
        {
            var user = await _userManager.FindByIdAsync(userID.ToString());
            if (user == null)
            {
                return false;
            }

            var claimExists = typeof(StaticRoleClaim).GetFields().Any(f => f.GetValue(null).ToString() == claim);
            if (!claimExists)
            {
                return false;
            }

            var claimObj = new Claim(ClaimTypes.Role, claim);
            await _userManager.RemoveClaimAsync(user, claimObj);

            return true;
        }

        public async Task<IEnumerable<string>?> GetUserClaim(Guid userID)
        {
            var user = await _userManager.FindByIdAsync(userID.ToString());
            if (user == null)
            {
                return null;
            }
            
            // get role
            var roles = await _userManager.GetRolesAsync(user);

            var claimList = new List<string>();
            
            foreach ( var role in roles)
            {
                // get role object
                var roleObj = await _roleManager.FindByNameAsync(role);
                if (roleObj == null)
                {
                    continue;
                }
                // get cliams
                var claims = await _roleManager.GetClaimsAsync(roleObj);
                var claimValues = claims.Select(c => c.Value);
                claimList.AddRange(claimValues);
            }

            // get user claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userClaimValues = userClaims.Select(c => c.Value);
            claimList.AddRange(userClaimValues);

            return claimList;

        }

       
    }
}
