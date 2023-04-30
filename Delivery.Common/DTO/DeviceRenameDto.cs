﻿using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO; 

/// <summary>
/// DTO for device rename
/// </summary>
public class DeviceRenameDto {
    /// <summary>
    /// Device name
    /// </summary>
    [Required]
    public String DeviceName { get; set; } = "";
}