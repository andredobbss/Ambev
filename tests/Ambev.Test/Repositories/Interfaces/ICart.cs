using Ambev.Domain.Entities;
using Ambev.Domain.ValueObjects;

namespace Ambev.Test.Repositories.Interfaces;

public interface ICart
{
    List<CartDomain> CartFake(List<CartProductDomain> products, int userId, DateTime date, bool cancel, int generate);
}
