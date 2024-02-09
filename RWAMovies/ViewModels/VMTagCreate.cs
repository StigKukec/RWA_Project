
namespace RWAMovies.ViewModels
{
    public class VMTagCreate
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<VMVideoTag> VideoTags { get; set; } = new List<VMVideoTag>();
    }
}
