using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Cart.Queries.Handlers;

public class GetCartsByFilterQueryHandler : IRequestHandler<GetCartsByFilterQuery, (IEnumerable<CartDomain> Data, int TotalItems, int TotalPages)>
{
    private readonly ICartService _cartService;

    public GetCartsByFilterQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<(IEnumerable<CartDomain> Data, int TotalItems, int TotalPages)> Handle(GetCartsByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var carts = await _cartService.GetCartsByFilterToPagedListAsync(
           request.FilterValue, request.FilterType, request.PageNumber, request.PageSize, request.OrderBy);

            return carts;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}

