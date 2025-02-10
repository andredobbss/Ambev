using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Cart.Queries.Handlers;

public class GetCartsByEspecialFiltersQueryHandler : IRequestHandler<GetCartsByEspecialFiltersQuery, IEnumerable<CartDomain>>
{
    private readonly ICartService _cartService;

    public GetCartsByEspecialFiltersQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IEnumerable<CartDomain>> Handle(GetCartsByEspecialFiltersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var carts = await _cartService.EspecialFiltersToCartsAsync(request.Filters);

            return carts;
        }
        catch (DomainValidationException)
        {

            throw;
        }      
    }
}
