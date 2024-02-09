
namespace RWAMovies.ViewModels
{
    public class VMGenreCreate
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<VMVideoGenre> VideoGenres { get; set; } = new List<VMVideoGenre>();
    }
}
