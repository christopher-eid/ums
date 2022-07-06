namespace Application.Exceptions;
class NotPossibleException : Exception
{
    public NotPossibleException() {  }

    public NotPossibleException(string message)
        : base(String.Format(message))
    {

    }
}