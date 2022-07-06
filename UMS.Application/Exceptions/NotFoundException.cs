namespace Application.Exceptions;
class NotFoundException : Exception
{
    public NotFoundException() {  }

    public NotFoundException(string message)
        : base(String.Format(message))
    {

    }
}