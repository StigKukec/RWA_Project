
namespace RWAMovies.ViewModels
{
    public class VMVideoGenre
    {
        public int IdvideoGenre { get; set; }

        public int VideoId { get; set; }

        public int GenreId { get; set; }

        public virtual VMGenre Genre { get; set; } = null!;

        public virtual VMVideo Video { get; set; } = null!;
    }
}
