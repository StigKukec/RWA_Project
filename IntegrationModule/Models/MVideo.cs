
namespace IntegrationModule.Models
{
    public class MVideo
    {
        public int Idvideo { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Image { get; set; }

        public int TotalTime { get; set; }

        public string StreamingUrl { get; set; } = null!;

        public virtual ICollection<MVideoGenre> VideoGenres { get; set; } = new List<MVideoGenre>();

        public virtual ICollection<MVideoTag> VideoTags { get; set; } = new List<MVideoTag>();
    }
}
