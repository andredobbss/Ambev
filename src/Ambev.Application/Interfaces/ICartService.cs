using Ambev.Application.Enums;
using Ambev.Domain.Entities;

namespace Ambev.Application.Interfaces;

public interface ICartService
{
    Task<IEnumerable<CartDomain>> EspecialFiltersToCartsAsync(Dictionary<string, string> filters);
    Task<(IEnumerable<CartDomain> Data, int TotalItems, int TotalPages)> GetCartsByFilterToPagedListAsync(object filterValue, ECartFilterType filterType, int pageNumber, int pageSize, string orderBy = null);
    Task<CartDomain> GetCartByIdAsync(int id);
    Task<CartDomain> AddCartAsync(CartDomain cartDomain);
    Task<CartDomain> UpdateCartAsync(CartDomain cartDomain);
    Task<CartDomain> DeleteCartAsync(int id);
}
