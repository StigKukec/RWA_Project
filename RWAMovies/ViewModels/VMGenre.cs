
namespace RWAMovies.ViewModels
{
    public class VMGenre
    {
        public int Idgenre { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<VMVideoGenre> VideoGenres { get; set; } = new List<VMVideoGenre>();
    }
}
