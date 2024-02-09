using AutoMapper;
using DataLayer.BLModels;
using RWAMovies.ViewModels;

namespace RWAMovies.Mapping
{
    public class AutomapperTag : Profile
    {
        public AutomapperTag()
        {
            CreateMap<BLTag, VMTag>();
        }
    }
}
