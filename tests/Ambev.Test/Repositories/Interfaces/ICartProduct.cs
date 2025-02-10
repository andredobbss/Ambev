using Ambev.Domain.Entities;
using Ambev.Domain.ValueObjects;

namespace Ambev.Test.Repositories.Interfaces;

public interface ICartProduct
{
    List<CartProductDomain> CartProductFake(List<ProductDomain> products, int generate);
}
