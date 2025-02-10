using Ambev.Domain.Entities;
using Ambev.Domain.IUnitOfWork;
using Ambev.Domain.Repositories;
using Ambev.Domain.Repositories.Auth;
using Ambev.Infraestructure.Database;
using Ambev.Infraestructure.Repositories;
using MongoDB.Driver;

namespace Ambev.Infraestructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly AppDbContext _appDbContext;

    private readonly IMongoCollection<UserDomain> _collection;

    private CartRepository _cartRepository;
    private ProductRepository _productRepository;
    private UserRepository _userRepository;

    private readonly IAuthenticationRepository _authentication;



    public UnitOfWork(IMongoDatabase mongoDatabase, AppDbContext appDbContext, IAuthenticationRepository authentication)
    {
        _mongoDatabase = mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase));
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));

        _collection = _mongoDatabase.GetCollection<UserDomain>("Users"); // Inicializa corretamente
    }


    public ICartRepository cartRepository
    {
        get
        {
            return _cartRepository = _cartRepository ?? new CartRepository(_mongoDatabase, _appDbContext);
        }
    }

    public IProductRepository productRepository
    {
        get
        {
            return _productRepository = _productRepository ?? new ProductRepository(_mongoDatabase, _appDbContext);
        }
    }

    public IUserRepository userRepository
    {
        get
        {
            return _userRepository = _userRepository ?? new UserRepository(_mongoDatabase, _appDbContext, _authentication, _collection);
        }
    }

    public async Task Commit()
    {
        await _appDbContext.SaveChangesAsync();

    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }
}
