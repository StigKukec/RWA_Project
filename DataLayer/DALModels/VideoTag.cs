using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class VideoTag
{
    public int IdvideoTag { get; set; }

    public int VideoId { get; set; }

    public int TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
