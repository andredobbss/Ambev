using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Cart.Commands.Handlers;

public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, CartDomain>
{
    private readonly ICartService _cartService;

    public DeleteCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDomain> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedCart = await _cartService.DeleteCartAsync(request.Id);

            return deletedCart;

        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
