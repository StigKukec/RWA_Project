

namespace RWAMovies.ViewModels
{
    public class VMCountry
    {
        public int Idcountry { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<VMUser> Users { get; set; } = new List<VMUser>();
    }
}
