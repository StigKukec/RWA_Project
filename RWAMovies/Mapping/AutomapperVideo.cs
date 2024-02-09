using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;
using RWAMovies.ViewModels;

namespace RWAMovies.Mapping
{
    public class AutomapperVideo : Profile
    {
        public AutomapperVideo()
        {

            CreateMap<BLVideo, VMVideo>()
            .ForMember(dest => dest.VideoGenres, opt => opt.MapFrom(src => src.VideoGenres.Select(
                vg => new VMVideoGenre
                {
                    IdvideoGenre = vg.IdvideoGenre,
                    GenreId = vg.GenreId,
                    VideoId = vg.VideoId,
                    Genre = new VMGenre
                    {
                        Idgenre = vg.Genre.Idgenre,
                        Name = vg.Genre.Name,
                        Description = vg.Genre.Description
                    }
                }))
            ).ForMember(dest => dest.VideoTags, opt => opt.MapFrom(src => src.VideoTags.Select(
                vt => new VMVideoTag
                {
                    IdvideoTag = vt.IdvideoTag,
                    TagId = vt.TagId,
                    VideoId = vt.VideoId,
                    Tag = new VMTag
                    {
                        Idtag = vt.Tag.Idtag,
                        Name = vt.Tag.Name
                    }
                }))
            );

        }
    }
}
