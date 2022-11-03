namespace DataAccess;

public interface IUnitOfWork
{
    public IQueryable<T> Collection<T>() where T : class;
    public T Create<T>(T entity) where T : class;
    public void Delete<T>(T entity) where T : class;
    public Task SaveAsync(CancellationToken cancellationToken = default);
}