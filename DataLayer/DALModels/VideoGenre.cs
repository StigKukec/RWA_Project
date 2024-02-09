using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class VideoGenre
{
    public int IdvideoGenre { get; set; }

    public int VideoId { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
