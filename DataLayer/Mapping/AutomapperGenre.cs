using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;

namespace DataLayer.Mapping
{
    public class AutomapperGenre : Profile
    {
        public AutomapperGenre()
        {
            CreateMap<Genre, BLGenre>()
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
                );
        }
    }
}
