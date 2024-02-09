using AutoMapper;
using DataLayer.BLModels;
using IntegrationModule.Models;

namespace IntegrationModule.Mapping
{
    public class AutomapperGenre : Profile
    {
        public AutomapperGenre()
        {
            CreateMap<BLGenre,MGenre>()
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
                );

        }
    }
}
