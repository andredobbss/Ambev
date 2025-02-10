using Ambev.Application.Enums;
using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.IUnitOfWork;
using Ambev.Domain.Resourcers;
using System.Linq.Expressions;

namespace Ambev.Application.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
  
    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;      
    }

    public async Task<IEnumerable<CartDomain>> EspecialFiltersToCartsAsync(Dictionary<string, string> filters)
    {
        var carts = await _unitOfWork.cartRepository.EspecialFiltersAsync(filters);
        return carts;
    }

    public async Task<(IEnumerable<CartDomain> Data, int TotalItems, int TotalPages)> GetCartsByFilterToPagedListAsync(object filterValue, ECartFilterType filterType, int pageNumber, int pageSize, string orderBy = null)
    {
        Expression<Func<CartDomain, bool>> filterExpression = u => true;

        switch (filterType)
        {
            case ECartFilterType.Cancel when filterValue is bool cancelValue:
                filterExpression = c => c.Cancel == cancelValue;
                break;

            case ECartFilterType.Date when filterValue is DateTime dateValue:
                filterExpression = c => c.Date == dateValue;
                break;

            case ECartFilterType.UserId when filterValue is int userIdValue:
                filterExpression = c => c.UserId == userIdValue;
                break;

            default:
                throw new KeyNotFoundException(ResourceMessagesException.INVALID_FILTER);
        }

        var carts = await _unitOfWork.cartRepository.GetToPagedListAsync(filterExpression, pageNumber, pageSize, orderBy);

        return (carts);
    }

    public async Task<CartDomain> GetCartByIdAsync(int id)
    {
        var cart = await _unitOfWork.cartRepository.GetSingleAsync(c => c.Id == id);

        return (cart);
    }

    public async Task<CartDomain> AddCartAsync(CartDomain cartDomain)
    {
        await _unitOfWork.cartRepository.Add(cartDomain);

        await _unitOfWork.Commit();

        return cartDomain;
    }

    public async Task<CartDomain> UpdateCartAsync(CartDomain cartDomain)
    {
      await  _unitOfWork.cartRepository.Update(cartDomain);

        await _unitOfWork.Commit();

        return cartDomain;
    }

    public async Task<CartDomain> DeleteCartAsync(int id)
    {
        var cartDomainResult = await _unitOfWork.cartRepository.GetSingleAsync(c => c.Id == id);

        if (cartDomainResult is null)
            return null;

       await _unitOfWork.cartRepository.Delete(cartDomainResult);

        await _unitOfWork.Commit();

        return cartDomainResult;
    }

}
