
namespace Tvitr.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string? ImageUrl { get; set; }
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
