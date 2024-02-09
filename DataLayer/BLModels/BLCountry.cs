

namespace DataLayer.BLModels
{
    public class BLCountry
    {
        public int Idcountry { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<BLUser> Users { get; set; } = new List<BLUser>();
    }
}
