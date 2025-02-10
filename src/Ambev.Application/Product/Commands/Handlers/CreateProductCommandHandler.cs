using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.Product.Commands.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDomain>
{
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDomain> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var productDomain = new ProductDomain(0,request.Title,request.Price,request.Description,request.Category,request.Image, new ProductRatingDomain(request.Rating.Rate,request.Rating.Count));

            var createdProduct = await _productService.AddProductAsync(productDomain);

            return createdProduct;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
