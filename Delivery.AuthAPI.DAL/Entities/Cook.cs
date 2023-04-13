namespace Delivery.AuthAPI.DAL.Entities; 

/// <summary>
/// Cook entity
/// </summary>
public class Cook {
    /// <summary>
    /// Cook`s id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Link to user
    /// </summary>
    public User? User { get; set; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="user"></param>
    public Cook(User user) {
        User = user;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    public Cook() {
    }
}