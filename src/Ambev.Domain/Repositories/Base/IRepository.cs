using System.Linq.Expressions;

namespace Ambev.Domain.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{

    Task<IEnumerable<TEntity>> EspecialFiltersAsync(Dictionary<string, string> filters);
    Task<(IEnumerable<TEntity> Data, int TotalItems, int TotalPages)> GetToPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, string orderBy = null);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
}
