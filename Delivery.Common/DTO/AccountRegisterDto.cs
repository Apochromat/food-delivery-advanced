﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Delivery.Common.Enums;

namespace Delivery.Common.DTO;

/// <summary>
/// DTO for user registration
/// </summary>
public class AccountRegisterDto {
    /// <summary>
    /// User`s email
    /// </summary>
    [Required]
    [EmailAddress]
    [DisplayName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// User`s password
    /// </summary>
    [Required]
    [DefaultValue("P@ssw0rd")]
    [DisplayName("password")]
    [MinLength(8)]
    public string? Password { get; set; }

    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    [Required]
    public string? FullName { get; set; }

    /// <summary>
    /// User`s birth date
    /// </summary>
    [Required]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// User`s gender
    /// </summary>
    [Required]
    public Gender Gender { get; set; }
}