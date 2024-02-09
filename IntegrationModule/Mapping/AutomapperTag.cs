using AutoMapper;
using DataLayer.BLModels;
using IntegrationModule.Models;

namespace IntegrationModule.Mapping
{
    public class AutomapperTag : Profile
    {
        public AutomapperTag()
        {
            CreateMap<BLTag,MTag>()
                 .ForMember(dest => dest.VideoTags, opt => opt.MapFrom(src => src.VideoTags.Select(
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
