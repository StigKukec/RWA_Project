
namespace IntegrationModule.Models
{
    public class MGenre
    {
        public int Idgenre { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<MVideoGenre> VideoGenres { get; set; } = new List<MVideoGenre>();
    }
}
