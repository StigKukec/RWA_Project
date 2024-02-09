﻿
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RWAMovies.ViewModels
{
    public class VMVideoCreate
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [DisplayName("Image")]
        [Url]
        public string? UrlImage { get; set; }

        [DisplayName("Total time")]
        public int TotalTime { get; set; }

        [DisplayName("Video reproduction url")]
        [Url]
        public string StreamingUrl { get; set; } = null!;

        [DisplayName("Genres")]
        public virtual ICollection<VMVideoGenre> VideoGenres { get; set; } = new List<VMVideoGenre>();

        [DisplayName("Tags")]
        public virtual ICollection<VMVideoTag> VideoTags { get; set; } = new List<VMVideoTag>();

    }
}
