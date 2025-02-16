namespace Portfi.Common.Exceptions;

public class NotAuthorizedException(string message)
    : Exception(message)
{ }