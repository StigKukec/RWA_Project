using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class Video
{
    public int Idvideo { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? UrlImage { get; set; }

    public int TotalTime { get; set; }

    public string StreamingUrl { get; set; } = null!;

    public virtual ICollection<VideoGenre> VideoGenres { get; set; } = new List<VideoGenre>();

    public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
}
