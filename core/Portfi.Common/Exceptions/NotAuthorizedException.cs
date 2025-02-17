namespace Portfi.Common.Exceptions;

/// <summary>
/// Exception thrown when a user attempts an unauthorized action.
/// </summary>
/// <param name="message">The error message describing the reason for the authorization failure.</param>
public class NotAuthorizedException(
    string message)
    : Exception(message)
{ }