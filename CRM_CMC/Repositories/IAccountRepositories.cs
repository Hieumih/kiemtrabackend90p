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

        // Role
        public Task<bool> SeedRole();
        public Task<IEnumerable<string>> ListRole();
        public Task<bool> AddRole(string role);
        public Task<bool> AddRoleClaim(string role, string claim);
        public Task<bool> RemoveRoleClaim(string role, string claim);
        public Task<IEnumerable<string>?> GetRoleClaim(string role);

        // user
        public Task<IEnumerable<string>?> GetUserRole(Guid userID);
        public Task<(string, bool)> AddUserToRole(Guid userID, string role);
        public Task<bool> AddUserClaim(Guid userID, string calim);
        public Task<bool> RemoveUserClaim(Guid userID, string claim);
        public Task<IEnumerable<string>?> GetUserClaim(Guid userID);
    }
}
