using AutoMapper;

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
        CreateMap<Delivery.Common.DTO.AccountRegisterDto, Delivery.AuthAPI.DAL.Entities.Customer>().ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.Email)
        );
    }
}