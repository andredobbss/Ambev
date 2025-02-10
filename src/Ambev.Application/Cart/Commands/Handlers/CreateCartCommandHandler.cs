using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.Cart.Commands.Handlers;

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartDomain>
{
    private readonly ICartService _cartService;

    public CreateCartCommandHandler(ICartService artService)
    {
        _cartService = artService;
    }

    public async Task<CartDomain> Handle(CreateCartCommand request, CancellationToken cancellationToken)
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
                0,
                request.UserId,
                request.Date,
                request.Cancel,
                products
            );

            var createCart = await _cartService.AddCartAsync(cartDomain);

            return createCart;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
