using Delivery.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Delivery.AuthAPI.DAL.Entities; 
/// <summary>
/// Users role
/// </summary>
public class Role : IdentityRole<Guid> {
    /// <summary>
    /// Role type
    /// </summary>
    public RoleType Type { get; set; }
    /// <summary>
    /// Users that have this role
    /// </summary>
    public ICollection<User>? Users { get; set; }
}