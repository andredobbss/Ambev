using Ambev.Application.Cart.Notifications;
using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Cart.Commands.Handlers;

public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, CartDomain>
{
    private readonly ICartService _cartService;
    private readonly IMediator _mediator;

    public DeleteCartCommandHandler(ICartService cartService, IMediator mediator)
    {
        _cartService = cartService;
        _mediator = mediator;
    }

    public async Task<CartDomain> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedCart = await _cartService.DeleteCartAsync(request.Id);

            await _mediator.Publish(new CartEvent(deletedCart), cancellationToken);

            return deletedCart;

        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
