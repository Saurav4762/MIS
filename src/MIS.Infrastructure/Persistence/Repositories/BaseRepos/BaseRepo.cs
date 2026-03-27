namespace MIS.Infrastructure.Persistence.Repositories.BaseRepo;

public class a
{
    
}

public class BaseRepo<T> where T : class
{
    protected async Task ExecuteAsync(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch 
        {
            
        }
    }
}
