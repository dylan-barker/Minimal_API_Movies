using Microsoft.AspNetCore.Identity;

namespace Minimal_API_Movies.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; } = null!;
        public int MovieId { get; set; }
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
}
