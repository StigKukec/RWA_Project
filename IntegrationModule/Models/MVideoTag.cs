
namespace IntegrationModule.Models
{
    public class MVideoTag
    {
        public int IdvideoTag { get; set; }

        public int VideoId { get; set; }

        public int TagId { get; set; }

        public virtual MTag Tag { get; set; } = null!;

        public virtual MVideo Video { get; set; } = null!;
    }
}
