namespace Delivery.AuthAPI.DAL.Entities; 

/// <summary>
/// Courier entity
/// </summary>
public class Courier {
    /// <summary>
    /// Courier`s id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Link to user
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="user"></param>
    public Courier(User user) {
        User = user;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    public Courier() {
    }
}