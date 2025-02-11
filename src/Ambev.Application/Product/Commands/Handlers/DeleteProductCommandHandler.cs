using Ambev.Application.Interfaces;
using Ambev.Application.Product.Notifications;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Product.Commands.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductDomain>
{
    private readonly IProductService _productService;
    private readonly IMediator _mediator;

    public DeleteProductCommandHandler(IProductService productService, IMediator mediator)
    {
        _productService = productService;
        _mediator = mediator;
    }

    public async Task<ProductDomain> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedProduct = await _productService.DeleteProductAsync(request.Id);

            await _mediator.Publish(new ProductEvent(deletedProduct), cancellationToken);

            return deletedProduct;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
