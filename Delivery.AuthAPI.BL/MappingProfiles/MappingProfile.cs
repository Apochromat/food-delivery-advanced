using AutoMapper;
using Delivery.AuthAPI.DAL.Entities;

namespace Delivery.AuthAPI.BL.MappingProfiles; 

/// <summary>
/// AuthAPI BL mapping profile 
/// </summary>
public class MappingProfile : Profile {
    /// <summary>
    /// Profile constructor
    /// </summary>
    public MappingProfile() {
        CreateMap<Delivery.AuthAPI.DAL.Entities.Device, Delivery.Common.DTO.DeviceDto>();
        CreateMap<Delivery.Common.DTO.DeviceDto, Delivery.AuthAPI.DAL.Entities.Device>();
        CreateMap<Delivery.Common.DTO.AccountRegisterDto, Delivery.AuthAPI.DAL.Entities.User>().ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.Email)
        );
        CreateMap<Delivery.AuthAPI.DAL.Entities.User, Delivery.Common.DTO.AccountProfileFullDto>();
        CreateMap<Delivery.AuthAPI.DAL.Entities.User, Delivery.Common.DTO.AccountCourierProfileDto>();
        CreateMap<Delivery.AuthAPI.DAL.Entities.User, Delivery.Common.DTO.AccountCookProfileDto>();
        CreateMap<Delivery.AuthAPI.DAL.Entities.User, Delivery.Common.DTO.AccountCustomerProfileDto>();
    }
}