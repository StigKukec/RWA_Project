

namespace DataLayer.BLModels
{
    public class BLVideo
    {
        public int Idvideo { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? UrlImage { get; set; }

        public int TotalTime { get; set; }

        public string StreamingUrl { get; set; } = null!;

        public virtual ICollection<BLVideoGenre> VideoGenres { get; set; } = new List<BLVideoGenre>();

        public virtual ICollection<BLVideoTag> VideoTags { get; set; } = new List<BLVideoTag>();
    }
}
