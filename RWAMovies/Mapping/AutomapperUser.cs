using AutoMapper;
using DataLayer.BLModels;
using DataLayer.DALModels;
using RWAMovies.ViewModels;
using System.Linq;

namespace RWAMovies.Mapping
{
    public class AutomapperUser : Profile
    {
        public AutomapperUser()
        {
            CreateMap<BLCountry, VMCountry>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            CreateMap<BLUserType, VMUserType>()
               .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<BLUser, VMUser>()
            .ForMember(dest => dest.UserNotifications, opt => opt.MapFrom(src => src.UserNotifications.Select(
               nf => new VMUserNotification
               {
                   IduserNotification = nf.IduserNotification,
                   UserId = nf.UserId,
                   NotificationId = nf.NotificationId,
                   Notification = new VMNotification
                   {
                       Idnotification = nf.Notification.Idnotification,
                       Guid = nf.Notification.Guid,
                       CreatedAt = nf.Notification.CreatedAt,
                       Sender = nf.Notification.Sender,
                       Receiver = nf.Notification.Receiver,
                       Subject = nf.Notification.Receiver,
                       Body = nf.Notification.Body,
                       SentAt = nf.Notification.SentAt
                   },
                   User = new VMUser
                   {
                       Iduser = nf.User.Iduser,
                       Created = nf.User.Created,
                       Username = nf.User.Username,
                       Email = nf.User.Email,
                       Verified = nf.User.Verified,
                       SecurityToken = nf.User.SecurityToken,
                       DeactivatedAt = nf.User.DeactivatedAt,
                       Deactivated = nf.User.Deactivated,
                       Country = new VMCountry
                       {
                           Idcountry = nf.User.Country.Idcountry,
                           Name = nf.User.Country.Name
                       }
                   }
               }))
           ).ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
           .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType));

            CreateMap<BLUser, VMUserProfile>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType));

            CreateMap<BLUser, VMUserEdit>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType));
        }
    }
}
