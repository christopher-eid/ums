namespace Application.Exceptions;
class AlreadyEnrolledException : Exception
{
    public AlreadyEnrolledException() {  }

    public AlreadyEnrolledException(string message)
        : base(String.Format(message))
    {

    }
}