
namespace IntegrationModule.Models
{
    public class MTag
    {
        public int Idtag { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<MVideoTag> VideoTags { get; set; } = new List<MVideoTag>();
    }
}
