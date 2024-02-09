using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;

namespace DataLayer.Mapping
{
    public class AutomapperNotification : Profile
    {
        public AutomapperNotification()
        {
            CreateMap<Notification,BLNotification>();
        }
    }
}
