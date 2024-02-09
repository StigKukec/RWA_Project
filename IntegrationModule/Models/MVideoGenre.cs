
namespace IntegrationModule.Models
{
    public class MVideoGenre
    {
        public int IdvideoGenre { get; set; }

        public int VideoId { get; set; }

        public int GenreId { get; set; }

        public virtual MGenre Genre { get; set; } = null!;

        public virtual MVideo Video { get; set; } = null!;
    }
}
