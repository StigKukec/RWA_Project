using AutoMapper;
using DataLayer.BLModels;
using RWAMovies.ViewModels;

namespace RWAMovies.Mapping
{
    public class AutomapperNotification : Profile
    {
        public AutomapperNotification()
        {
            CreateMap<BLNotification, VMNotification>();
        }
    }
}
