using Ambev.Application.Cart.Notifications;
using Ambev.Application.Interfaces;
using Ambev.Application.Product.Notifications;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.Product.Commands.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDomain>
{
    private readonly IProductService _productService;
    private readonly IMediator _mediator;

    public CreateProductCommandHandler(IProductService productService, IMediator mediator)
    {
        _productService = productService;
        _mediator = mediator;
    }

    public async Task<ProductDomain> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var productDomain = new ProductDomain(0,request.Title,request.Price,request.Description,request.Category,request.Image, new ProductRatingDomain(request.Rating.Rate,request.Rating.Count));

            var createdProduct = await _productService.AddProductAsync(productDomain);

            await _mediator.Publish(new ProductEvent(createdProduct), cancellationToken);

            return createdProduct;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
