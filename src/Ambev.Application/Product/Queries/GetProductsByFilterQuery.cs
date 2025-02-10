using Ambev.Application.Enums;
using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Product.Queries;

public class GetProductsByFilterQuery : IRequest<(IEnumerable<ProductDomain> Data, int TotalItems, int TotalPages)>
{
    public GetProductsByFilterQuery(object filterValue, EProductFilterType filterType, int pageNumber, int pageSize, string orderBy = null)
    {
        FilterValue = filterValue;
        FilterType = filterType;
        PageNumber = pageNumber;
        PageSize = pageSize;
        OrderBy = orderBy;
    }

    public object FilterValue { get; }
    public EProductFilterType FilterType { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public string OrderBy { get; }
}
