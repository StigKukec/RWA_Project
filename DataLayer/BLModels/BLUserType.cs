using DataLayer.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BLModels
{
    public class BLUserType
    {
        public int IduserType { get; set; }

        public string Type { get; set; } = null!;

        public virtual ICollection<BLUser> Users { get; set; } = new List<BLUser>();
    }
}
