namespace NdbPortal.Web.exceptions;

public class TokenValidationException : Exception
{
    public TokenValidationException(string message) : base(message)
    {
    }
}