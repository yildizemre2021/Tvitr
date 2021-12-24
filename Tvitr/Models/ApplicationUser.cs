using Microsoft.AspNetCore.Identity;

namespace Tvitr.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }

        public ICollection<Tweet>? Tweets { get; set; }
    }
}
