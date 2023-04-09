using AutoMapper;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;

namespace Delivery.AdminPanel.BL.MappingProfiles; 

/// <summary>
/// AuthAPI BL mapping profile 
/// </summary>
public class MappingProfile : Profile {
    /// <summary>
    /// Profile constructor
    /// </summary>
    public MappingProfile() {
        CreateMap<Restaurant, RestaurantShortDto>();
        CreateMap<Restaurant, RestaurantFullDto>();
        CreateMap<User, AccountProfileFullDto>();
    }
}