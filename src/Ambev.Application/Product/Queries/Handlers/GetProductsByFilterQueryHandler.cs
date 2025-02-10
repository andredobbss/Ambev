using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.Product.Queries.Handlers;

public class GetProductsByFilterQueryHandler : IRequestHandler<GetProductsByFilterQuery, (IEnumerable<ProductDomain> Data, int TotalItems, int TotalPages)>
{
    private readonly IProductService _productService;

    public GetProductsByFilterQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<(IEnumerable<ProductDomain> Data, int TotalItems, int TotalPages)> Handle(GetProductsByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productService.GetProductsByFilterToPagedListAsync(request.FilterValue, request.FilterType, request.PageNumber, request.PageSize, request.OrderBy);

            return products;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
