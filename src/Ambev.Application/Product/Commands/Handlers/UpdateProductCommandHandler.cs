using Ambev.Application.Cart.Notifications;
using Ambev.Application.Interfaces;
using Ambev.Application.Product.Notifications;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.Product.Commands.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDomain>
{
    private readonly IProductService _productService;
    private readonly IMediator _mediator;

    public UpdateProductCommandHandler(IProductService productService, IMediator mediator)
    {
        _productService = productService;
        _mediator = mediator;
    }

    public async Task<ProductDomain> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var productDomain = new ProductDomain(request.Id, request.Title, request.Price, request.Description, request.Category, request.Image, new ProductRatingDomain(request.Rating.Rate, request.Rating.Count));

            var updatedProduct = await _productService.UpdateProductAsync(productDomain);

            await _mediator.Publish(new ProductEvent(updatedProduct), cancellationToken);

            return updatedProduct;

        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
