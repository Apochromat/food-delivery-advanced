namespace Delivery.Common.Exceptions;

/// <summary>
/// Exception for conflict HTTP status code
/// </summary>
[Serializable]
public class IncorrectLoginException : Exception {
    /// <summary>
    /// Constructor
    /// </summary>
    public IncorrectLoginException() {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public IncorrectLoginException(string message)
        : base(message) {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public IncorrectLoginException(string message, Exception inner)
        : base(message, inner) {
    }
}