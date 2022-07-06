namespace Application.Exceptions;
class InvalidIdentifierException : Exception
{
    public InvalidIdentifierException() {  }

    public InvalidIdentifierException(string message)
        : base(String.Format(message))
    {

    }
}