namespace MIS.Domain.Exceptions;

public class DatabaseException : BaseException
{
    public DatabaseException() :
        base(
            "Database error occured",
            "DATABASE"
        )
    {
        
    }
}