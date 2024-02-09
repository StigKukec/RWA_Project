
namespace RWAMovies.ViewModels
{
    public class VMTag
    {
        public int Idtag { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<VMVideoTag> VideoTags { get; set; } = new List<VMVideoTag>();
    }
}
