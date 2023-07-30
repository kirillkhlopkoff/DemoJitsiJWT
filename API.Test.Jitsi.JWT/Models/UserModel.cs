using Microsoft.AspNetCore.Identity;

namespace API.Test.Jitsi.JWT.Models
{
    public class UserModel:IdentityUser
    {
        public string? UserAvatar { get; set; }
        public bool isModerator { get; set; }
    }
}
