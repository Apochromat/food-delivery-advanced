using AutoMapper;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;

namespace Delivery.BackendAPI.BL.MappingProfiles; 

/// <summary>
/// Backend API mapping profile
/// </summary>
public class MappingProfile : Profile {
    /// <summary>
    /// Constructor
    /// </summary>
    public MappingProfile() {
        CreateMap<Restaurant, RestaurantShortDto>();
    }
}