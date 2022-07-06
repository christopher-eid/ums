namespace Application.Exceptions;
class AlreadyExistingException : Exception
{
    public AlreadyExistingException() {  }

    public AlreadyExistingException(string message)
        : base(String.Format(message))
    {

    }
}