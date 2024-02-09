

namespace DataLayer.BLModels
{
    public class BLTag
    {
        public int Idtag { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<BLVideoTag> VideoTags { get; set; } = new List<BLVideoTag>();
    }
}
