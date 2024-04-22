using Microsoft.AspNetCore.Identity;

namespace CRM_CMC.Models
{
    public class UserRole : IdentityRole
    {
        public bool canView { get; set; }
        public bool canEdit { get; set; }
        public bool canDelete { get; set; }
    }
}
