using Ambev.Application.Interfaces;
using Ambev.Application.User.Notifcations;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Cart.Queries.Handlers;

public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartDomain>
{
    private readonly ICartService _cartService;

    public GetCartByIdQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDomain> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await _cartService.GetCartByIdAsync(request.Id);

            return cart;
        }
        catch (DomainValidationException)
        {

            throw;
        }
       
    }
}
