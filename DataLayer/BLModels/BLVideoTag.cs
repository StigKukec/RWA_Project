using DataLayer.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BLModels
{
    public class BLVideoTag
    {
        public int IdvideoTag { get; set; }

        public int VideoId { get; set; }

        public int TagId { get; set; }

        public virtual BLTag Tag { get; set; } = null!;

        public virtual BLVideo Video { get; set; } = null!;
    }
}
