using Ambev.Application.Enums;
using Ambev.Domain.Entities;

namespace Ambev.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDomain>> EspecialFiltersToProductsAsync(Dictionary<string, string> filters);
    Task<(IEnumerable<ProductDomain> Data, int TotalItems, int TotalPages)> GetProductsByFilterToPagedListAsync(object filterValue, EProductFilterType filterType, int pageNumber, int pageSize, string orderBy = null);
    Task<ProductDomain> GetProductByIdAsync(int id);
    Task<ProductDomain> AddProductAsync(ProductDomain productDomain);
    Task<ProductDomain> UpdateProductAsync(ProductDomain productDomain);
    Task<ProductDomain> DeleteProductAsync(int id);
}
