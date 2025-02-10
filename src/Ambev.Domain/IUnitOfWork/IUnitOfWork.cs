using Ambev.Domain.Repositories;

namespace Ambev.Domain.IUnitOfWork;

public interface IUnitOfWork
{
    ICartRepository cartRepository { get; }
    IProductRepository productRepository { get; }
    IUserRepository userRepository { get; }
    Task Commit();
    void Dispose();
}
