namespace Minimal_API_Movies.Entities
{
    public class ActorMovie
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public int Order { get; set; }
        public string Character { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
