using CRM_CMC.App_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CRM_CMC.Repositories
{
    public interface IAccountRepositories
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<JwtSecurityToken?> SignInAsync(SignInModel model);

        public Task<bool> SeedRole();

        public Task<(string, bool)> SetRole(Guid userID, string role);

        public Task<IEnumerable<string>?> GetRole(Guid userID);

    }
}
