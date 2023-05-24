using Microsoft.AspNetCore.Identity;

namespace SocialLogin.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
