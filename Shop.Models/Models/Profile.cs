using Microsoft.AspNetCore.Identity;

namespace Shop.Models.Models
{
    public class Profile : IdentityUser
    {
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
