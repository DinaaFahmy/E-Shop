using Microsoft.AspNetCore.Identity;

namespace Shop.Models.Models
{
    public class Role : IdentityRole
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }

        public override string ToString()
        {
            return Name;
        }
    }
}
