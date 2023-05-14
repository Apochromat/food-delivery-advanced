using Delivery.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Delivery.AuthAPI.DAL.Entities;

/// <summary>
/// AuthDB general User Model   
/// </summary>
public class User : IdentityUser<Guid> {
    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// User`s birth date
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// User`s gender
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Date when user joined the system
    /// </summary>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User`s devices
    /// </summary>
    public List<Device> Devices { get; set; } = new List<Device>();

    /// <summary>
    /// Link to customer
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Link to courier
    /// </summary>
    public Courier? Courier { get; set; }

    /// <summary>
    /// Link to manager
    /// </summary>
    public Manager? Manager { get; set; }

    /// <summary>
    /// Link to cook
    /// </summary>
    public Cook? Cook { get; set; }
}