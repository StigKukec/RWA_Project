using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;

namespace DataLayer.Mapping
{
    public class AutomapperUser : Profile
    {
        public AutomapperUser()
        {
            CreateMap<Country, BLCountry>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            CreateMap<UserType, BLUserType>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<User, BLUser>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType));
        }
    }
}
