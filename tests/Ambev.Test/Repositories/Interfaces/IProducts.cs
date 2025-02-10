using Ambev.Domain.Entities;

namespace Ambev.Test.Repositories.Interfaces;

public interface IProducts
{
    List<ProductDomain> ProdcutsFake(int generate);
}
