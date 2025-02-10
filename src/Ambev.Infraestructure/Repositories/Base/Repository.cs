using Ambev.Domain.Entities.Base;
using Ambev.Domain.Repositories.Auth;
using Ambev.Domain.Repositories.Base;
using Ambev.Infraestructure.Database;
using Ambev.Infraestructure.Repositories.Filter;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;


namespace Ambev.Infraestructure.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly IMongoCollection<TEntity> _collection;

    private readonly MongoFilterService<TEntity> _filterService;

    private readonly IAuthenticationRepository _authentication;

    private readonly AppDbContext _appDbContext;


    public IMongoDatabase Database { get; }


    public Repository(AppDbContext appDbContext, IAuthenticationRepository authentication, IMongoCollection<TEntity> collection)
    {
        _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
    }

    public Repository(IMongoDatabase database, AppDbContext appDbContext, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);

        _appDbContext = appDbContext;
    }

    public Repository(MongoFilterService<TEntity> filterService)
    {
        _filterService = filterService;
    }

    public async Task<IEnumerable<TEntity>> EspecialFiltersAsync(Dictionary<string, string> filters)
    {
        return await _filterService.ApplyFilters(filters);
    }


    public async Task<(IEnumerable<TEntity> Data, int TotalItems, int TotalPages)> GetToPagedListAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, string orderBy = null)
    {
        var filter = Builders<TEntity>.Filter.Where(predicate);
        var totalItems = (int)await _collection.CountDocumentsAsync(filter);

        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var query = _collection.Find(filter);

        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderParams = orderBy.Split(',');
            foreach (var param in orderParams)
            {
                var trimmedParam = param.Trim();
                bool descending = trimmedParam.EndsWith("desc");
                var propertyName = trimmedParam.Split(' ')[0];

                var sortDefinition = descending
                    ? Builders<TEntity>.Sort.Descending(propertyName)
                    : Builders<TEntity>.Sort.Ascending(propertyName);
                query = query.Sort(sortDefinition);
            }
        }

        var data = await query.Skip((pageNumber - 1) * pageSize)
                              .Limit(pageSize)
                              .ToListAsync();

        return (data, totalItems, totalPages);
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = await _collection.Find(predicate).FirstOrDefaultAsync();

        return query;
    }


    public async Task Add(TEntity entity)
    {

      
        await Task.Run(() =>
        {
            _appDbContext.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Added;
            _appDbContext.Set<TEntity>().Add(entity);
        });

        //Agora, recupere o Id gerado no PostgreSQL

        var entityWithId = _appDbContext.Entry(entity).Entity;

        if (entityWithId is IEntity identifiableEntity && (int)identifiableEntity.GetId() == 0)
        {
            identifiableEntity.GetType().GetProperty("Id")?.SetValue(identifiableEntity, GetUniqueId());
        }

        // Persistir no MongoDB
        await _collection.InsertOneAsync(entityWithId);

    }

    public async Task Update(TEntity entity)
    {
        await Task.Run(() =>
        {
            _appDbContext.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Set<TEntity>().Update(entity);
        });

        var filter = Builders<TEntity>.Filter.Eq("Id", entity.GetId()); 
        var update = Builders<TEntity>.Update
            .Set(e => e, entity);

        await _collection.UpdateOneAsync(filter, update);

    }

    public async Task Delete(TEntity entity)
    {
        await Task.Run(() =>
        {
            _appDbContext.Entry(entity).State = EntityState.Deleted;
            _appDbContext.Set<TEntity>().Remove(entity);
        });

        var filter = Builders<TEntity>.Filter.Eq("Id", entity.GetId());

        await _collection.DeleteOneAsync(filter);
    }


    private int GetUniqueId()
{
    // Você pode gerar um ID único com base em um contador, timestamp ou qualquer outra lógica
    return new Random().Next(1, int.MaxValue); // Exemplo simples de geração de ID único
}

}

