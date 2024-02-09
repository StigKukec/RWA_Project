using AutoMapper;
using DataLayer.BLModels;
using IntegrationModule.Models;

namespace IntegrationModule.Mapping
{
    public class AutomapperVideo : Profile
    {
        public AutomapperVideo()
        {
            CreateMap<BLVideo, MVideo>()
                 .ForMember(dest => dest.VideoGenres, opt => opt.MapFrom(src => src.VideoGenres.Select(
                    vg => new MVideoGenre
                    {
                        IdvideoGenre = vg.IdvideoGenre,
                        GenreId = vg.GenreId,
                        VideoId = vg.VideoId,
                        Genre = new MGenre
                        {
                            Idgenre = vg.Genre.Idgenre,
                            Name = vg.Genre.Name,
                            Description = vg.Genre.Description
                        }
                    }))
                ).ForMember(dest => dest.VideoTags, opt => opt.MapFrom(src => src.VideoTags.Select(
                    vt => new MVideoTag
                    {
                        IdvideoTag = vt.IdvideoTag,
                        TagId = vt.TagId,
                        VideoId = vt.VideoId,
                        Tag = new MTag
                        {
                            Idtag = vt.Tag.Idtag,
                            Name = vt.Tag.Name,
                        }
                    }))
                );
        }
    }
}
