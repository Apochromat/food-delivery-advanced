using System.ComponentModel.DataAnnotations;
using Delivery.Common.Enums;

namespace Delivery.AdminPanel.Models; 

public class UserEditModel {
    public Guid Id { get; set; }
    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    [Required]
    public string FullName { get; set; } = "";
    /// <summary>
    /// User`s gender
    /// </summary>
    [Required]
    public Gender Gender { get; set; }
    /// <summary>
    /// User roles
    /// </summary>
    [Required]
    public List<String> Roles { get; set; } = new List<String>();
}