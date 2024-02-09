using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;

namespace DataLayer.Mapping
{
    public class AutomapperVideo : Profile
    {
        public AutomapperVideo()
        {
            CreateMap<Video, BLVideo>()
                .ForMember(dest => dest.VideoGenres, opt => opt.MapFrom(src => src.VideoGenres.Select(
                    vg => new BLVideoGenre
                    {
                        IdvideoGenre = vg.IdvideoGenre,
                        GenreId = vg.GenreId,
                        VideoId = vg.VideoId,
                        Genre = new BLGenre
                        {
                            Idgenre = vg.Genre.Idgenre,
                            Name = vg.Genre.Name,
                            Description = vg.Genre.Description
                        }
                    }))
                ).ForMember(dest => dest.VideoTags, opt => opt.MapFrom(src => src.VideoTags.Select(
                    vt => new BLVideoTag
                    {
                        IdvideoTag = vt.IdvideoTag,
                        TagId = vt.TagId,
                        VideoId = vt.VideoId,
                        Tag = new BLTag
                        {
                            Idtag = vt.Tag.Idtag,
                            Name = vt.Tag.Name,
                        }
                    }))
                );
            //CreateMap<IEnumerable<Video>, IEnumerable<BLVideo>>();
        }
    }
}
