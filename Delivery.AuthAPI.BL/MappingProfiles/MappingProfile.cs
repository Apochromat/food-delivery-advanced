using AutoMapper;

namespace Delivery.AuthAPI.BL.MappingProfiles; 

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Delivery.AuthAPI.DAL.Entities.Device, Delivery.Common.DTO.DeviceDto>();
        CreateMap<Delivery.Common.DTO.DeviceDto, Delivery.AuthAPI.DAL.Entities.Device>();
    }
}