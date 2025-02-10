using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.Product.Commands.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDomain>
{
    private readonly IProductService _productService;

    public UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDomain> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var productDomain = new ProductDomain(request.Id, request.Title, request.Price, request.Description, request.Category, request.Image, new ProductRatingDomain(request.Rating.Rate, request.Rating.Count));

            var updatedProduct = await _productService.UpdateProductAsync(productDomain);

            return updatedProduct;

        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
