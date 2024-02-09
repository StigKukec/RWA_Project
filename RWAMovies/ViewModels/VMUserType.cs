using DataLayer.BLModels;

namespace RWAMovies.ViewModels
{
    public class VMUserType
    {
        public int IduserType { get; set; }

        public string Type { get; set; } = null!;

        public virtual ICollection<VMUser> Users { get; set; } = new List<VMUser>();
    }
}
