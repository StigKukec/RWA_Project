using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;

namespace DataLayer.Mapping
{
    public class AutomapperTag : Profile
    {
        public AutomapperTag()
        {
            CreateMap<Tag, BLTag>()
                .ForMember(dest => dest.VideoTags, opt => opt.MapFrom(src => src.VideoTags.Select(
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
        }
    }
}
