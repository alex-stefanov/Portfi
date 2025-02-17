namespace Portfi.Common.Exceptions;

/// <summary>
/// Exception thrown when an item could not be deleted.
/// </summary>
/// <param name="message">The error message describing the reason for the failure.</param>
public class ItemNotDeletedException(
    string message)
    : Exception(message)
{ }