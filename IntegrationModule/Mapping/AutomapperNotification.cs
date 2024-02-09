using AutoMapper;
using DataLayer.BLModels;
using IntegrationModule.Models;

namespace IntegrationModule.Mapping
{
    public class AutomapperNotification : Profile
    {
        public AutomapperNotification()
        {
            CreateMap<BLNotification,MNotification>();
        }
    }
}
