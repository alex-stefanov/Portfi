namespace Portfi.Common.Exceptions;

public class ItemNotDeletedException(string message)
    : Exception(message)
{ }