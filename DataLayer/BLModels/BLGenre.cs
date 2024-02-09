using DataLayer.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BLModels
{
    public class BLGenre
    {
        public int Idgenre { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<BLVideoGenre> VideoGenres { get; set; } = new List<BLVideoGenre>();
    }
}
