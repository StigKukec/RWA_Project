using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class Tag
{
    public int Idtag { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
}
