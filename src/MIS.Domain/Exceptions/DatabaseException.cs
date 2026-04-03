namespace MIS.Domain.Exceptions;

public class DatabaseException : BaseException
{
    public DatabaseException() :
        base(
            "Something went worng",
            "SERVER"
        )
    {

    }
    public DatabaseException(string message) : base(message, "SERVER") {}
}