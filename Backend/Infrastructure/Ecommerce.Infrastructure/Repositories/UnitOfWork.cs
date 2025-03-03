using System.Collections;
using Ecommerce.Application.Persistences;
using Ecommerce.Infrastructure.Persistences;

namespace Ecommerce.Infrastructure.Repositories;

public class UnitOfWork(EcommerceDbContext context) : IUnitOfWork
{
    private readonly Hashtable _repositories = new(); 
    private readonly EcommerceDbContext _context = context;

    public async Task<int> Complete()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured during completing the transaction: {e}");
        }
    }

    public void Dispose() => _context.Dispose();

    public IAsyncRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(AsyncRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);
        }
        return (IAsyncRepository<T>)_repositories[type]!;
    }
}