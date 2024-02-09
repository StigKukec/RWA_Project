using AutoMapper;
using DataLayer.BLModels;
using RWAMovies.ViewModels;

namespace RWAMovies.Mapping
{
    public class AutomapperGenre : Profile
    {
        public AutomapperGenre()
        {
            CreateMap<BLGenre, VMGenre>();
        }
    }
}
