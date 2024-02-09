
namespace RWAMovies.ViewModels
{
    public class VMVideoTag
    {
        public int IdvideoTag { get; set; }

        public int VideoId { get; set; }

        public int TagId { get; set; }

        public virtual VMTag Tag { get; set; } = null!;

        public virtual VMVideo Video { get; set; } = null!;
    }
}
