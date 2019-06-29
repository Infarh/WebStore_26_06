using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string AdminUserName = "Admin";
        public const string DefaultAdminPassword = "AdminPassword";

        public const string RoleAdmin = "Administrator";
        public const string RoleUser = "User";

    }

    //public class Role : IdentityRole { }
}
