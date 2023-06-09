﻿using Delivery.Common.Enums;

namespace Delivery.Common.DTO;

/// <summary>
/// Account profile DTO
/// </summary>
public class AccountProfileFullDto {
    /// <summary>
    /// Profile Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User email
    /// </summary>
    public required string Email { get; set; }

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
    /// Is user banned
    /// </summary>
    public Boolean IsBanned { get; set; }

    /// <summary>
    /// Date when user joined the system
    /// </summary>
    public DateTime JoinedAt { get; set; }

    /// <summary>
    /// User roles
    /// </summary>
    public List<string> Roles { get; set; } = new();
}