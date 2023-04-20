using AutoMapper;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;

namespace Delivery.AuthAPI.BL.MappingProfiles; 

/// <summary>
/// AuthAPI BL mapping profile 
/// </summary>
public class MappingProfile : Profile {
    /// <summary>
    /// Profile constructor
    /// </summary>
    public MappingProfile() {
        CreateMap<Device, DeviceDto>();
        CreateMap<DeviceDto, Device>();
        CreateMap<AccountRegisterDto, User>().ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.Email)
        );
        CreateMap<User, AccountProfileFullDto>();
        CreateMap<User, AccountCourierProfileDto>();
        CreateMap<User, AccountCookProfileDto>();
        CreateMap<User, AccountCustomerProfileDto>();
        CreateMap<Customer, AccountCustomerProfileFullDto>();
    }
}