using System.Linq.Expressions;
using Ecommerce.Application.Specifications;

namespace Ecommerce.Application.Persistences;

public interface IAsyncRepository<T> where T : class 
{
    Task<IReadOnlyList<T>> GetAllAsync();
    
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate
    );
    
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        string? includeString,
        bool disableTracking = true
    );
    
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        List<Expression<Func<T, object>>>? includes = null,
        bool disableTracking = true
    );
    
    Task<T> GetEntityAsync(
        Expression<Func<T, bool>>? predicate, 
        List<Expression<Func<T, object>>>? includes = null, 
        bool disableTracking = true
    );
    
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    void AddEntity(T entity);
    void UpdateEntity(T entity);
    void DeleteEntity(T entity);
    void AddRange(List<T> entities);
    void DeleteRange(IReadOnlyList<T> entities);
    
    Task<T> GetByIdWithSpecification(ISpecification<T> specification);
    Task<IReadOnlyList<T>> GetAllWithSpecification(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
}