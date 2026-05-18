namespace Minimal_API_Movies.DTOs
{
    public class CreateCommentDTO
    {
        public string Body { get; set; } = null!;
        public int MovieId { get; set; }
    }
}
