using Microsoft.EntityFrameworkCore;
using MIS.Domain.Exceptions;

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
            throw new DatabaseException();
        }
    }
}
