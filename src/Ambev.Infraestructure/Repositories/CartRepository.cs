using Ambev.Domain.Entities;
using Ambev.Domain.Repositories;
using Ambev.Infraestructure.Database;
using Ambev.Infraestructure.Repositories.Base;
using MongoDB.Driver;

namespace Ambev.Infraestructure.Repositories;

public class CartRepository : Repository<CartDomain>, ICartRepository
{
    public CartRepository(IMongoDatabase database, AppDbContext appDbContext) : base(database, appDbContext, "Carts")
    {
    }
}
