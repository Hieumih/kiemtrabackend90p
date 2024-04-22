using Microsoft.AspNetCore.Identity;

namespace CRM_CMC.Models
{
    public class UserRole : IdentityRole
    {
        public string RoleName { get; set; } = null!;
        public UserRole() : base()
        {

        }
        public UserRole(string roleName) : base(roleName)
        {
        }


    }
}
