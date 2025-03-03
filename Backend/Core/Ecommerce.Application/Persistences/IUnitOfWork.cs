namespace Ecommerce.Application.Persistences;

public interface IUnitOfWork : IDisposable
{
    IAsyncRepository<T> Repository<T>() where T : class;
    Task<int> Complete();
}