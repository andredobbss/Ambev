using Ambev.Application.Cart.Notifications;
using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.Cart.Commands.Handlers;

public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, CartDomain>
{
    private readonly ICartService _cartService;
    private readonly IMediator _mediator;

    public UpdateCartCommandHandler(ICartService artService, IMediator mediator)
    {
        _cartService = artService;
        _mediator = mediator;
    }

    public async Task<CartDomain> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var products = request.Products?
             .Select(p => new CartProductDomain(
                 new ProductDomain(p.ProductId, p.Title, p.Price, p.Description, p.Category, p.Image, new ProductRatingDomain(p.Rating.Rate, p.Rating.Count)),
                 p.Quantity,
                 p.Subsidiary ?? string.Empty))
             .ToList();

            var cartDomain = new CartDomain(
                request.Id,
                request.UserId,
                request.Date,
                request.Cancel,
                products
            );

            var updatedCart = await _cartService.UpdateCartAsync(cartDomain);

            await _mediator.Publish(new CartEvent(updatedCart), cancellationToken);

            return updatedCart;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
