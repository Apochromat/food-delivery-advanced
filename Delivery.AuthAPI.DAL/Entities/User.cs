﻿using Delivery.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Delivery.AuthAPI.DAL.Entities;

/// <summary>
/// AuthDB general User Model   
/// </summary>
public class User : IdentityUser<Guid> {
    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    public string FullName { get; set; } = "";
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
    public DateTime JoinedAt { get; set; }
    /// <summary>
    /// List of roles that user has
    /// </summary>
    public ICollection<Role>? Roles { get; set; }
}