namespace Delivery.AuthAPI.DAL.Entities; 

/// <summary>
/// Manager entity
/// </summary>
public class Manager {
    /// <summary>
    /// Manager`s id
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
    public Manager(User user) {
        User = user;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    public Manager() {
    }
}