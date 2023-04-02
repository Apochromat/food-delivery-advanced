using AutoMapper;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;

namespace Delivery.BackendAPI.BL.MappingProfiles; 

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Restaurant, RestaurantShortDto>();
    }
}