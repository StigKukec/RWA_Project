

namespace DataLayer.BLModels
{
    public class BLVideoGenre
    {
        public int IdvideoGenre { get; set; }

        public int VideoId { get; set; }

        public int GenreId { get; set; }

        public virtual BLGenre Genre { get; set; } = null!;

        public virtual BLVideo Video { get; set; } = null!;
    }
}
