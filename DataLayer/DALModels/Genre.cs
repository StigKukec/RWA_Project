using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class Genre
{
    public int Idgenre { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<VideoGenre> VideoGenres { get; set; } = new List<VideoGenre>();
}
