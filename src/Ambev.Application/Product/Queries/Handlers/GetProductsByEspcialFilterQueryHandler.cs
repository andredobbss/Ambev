using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Product.Queries.Handlers;

public class GetProductsByEspcialFilterQueryHandler : IRequestHandler<GetProductsByEspcialFilterQuery, IEnumerable<ProductDomain>>
{
    private readonly IProductService _productService;

    public GetProductsByEspcialFilterQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IEnumerable<ProductDomain>> Handle(GetProductsByEspcialFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productService.EspecialFiltersToProductsAsync(request.Filters);

            return products;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
