using CRM_CMC.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace CRM_CMC.Repositories
{
    public interface IAccountRepositories
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<JwtSecurityToken?> SignInAsync(SignInModel model);
    }
}
