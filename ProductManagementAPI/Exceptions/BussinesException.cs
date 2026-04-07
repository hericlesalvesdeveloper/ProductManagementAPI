namespace ProductManagementAPI.Exceptions;

public class BussinesException : Exception
{
    public BussinesException(string message)
        : base(message)
    {
    }
}
