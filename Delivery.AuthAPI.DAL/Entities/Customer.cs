using System.ComponentModel.DataAnnotations;

namespace Delivery.AuthAPI.DAL.Entities; 

/// <summary>
/// Customer entity
/// </summary>
public class Customer {
    /// <summary>
    /// Customer`s id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Link to user
    /// </summary>
    public User? User { get; set; }
    /// <summary>
    /// Customer`s address 
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="user"></param>
    public Customer(User user) {
        User = user;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    public Customer() {
    }
}