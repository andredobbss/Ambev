using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Product.Commands.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductDomain>
{
    private readonly IProductService _productService;
  
    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDomain> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedProduct = await _productService.DeleteProductAsync(request.Id);

            return deletedProduct;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
