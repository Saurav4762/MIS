using Microsoft.EntityFrameworkCore;
using MIS.Domain.Exceptions;
using Npgsql;

namespace MIS.Infrastructure.Persistence.Repositories.BaseRepos;

public class BaseRepo<T> where T : class
{
    protected async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action)
    {
        try
        {
            return await action();
        }
        
        catch (DbUpdateException ex)
        {
            if(ex.InnerException is PostgresException pgEx)
            {
                if(pgEx.SqlState == "23505")
                {

                }
            }
            throw new DatabaseException();
        }
    }
}
