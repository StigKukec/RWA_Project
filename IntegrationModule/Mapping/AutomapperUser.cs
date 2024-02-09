using AutoMapper;
using DataLayer.BLModels;
using IntegrationModule.Models;

namespace IntegrationModule.Mapping
{
    public class AutomapperUser : Profile
    {
        public AutomapperUser()
        {
            CreateMap<BLUser, MUserSignInRequest>();
            CreateMap<BLUser, MUser>();
        }
    }
}
