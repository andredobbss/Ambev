using Ambev.Application.Enums;
using Ambev.Domain.Entities;
using MediatR;
namespace Ambev.Application.Cart.Queries;

public class GetCartsByFilterQuery : IRequest<(IEnumerable<CartDomain> Data, int TotalItems, int TotalPages)>
{
    public object FilterValue { get; }
    public ECartFilterType FilterType { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public string OrderBy { get; }

    public GetCartsByFilterQuery(object filterValue, ECartFilterType filterType, int pageNumber, int pageSize, string orderBy = null)
    {
        FilterValue = filterValue;
        FilterType = filterType;
        PageNumber = pageNumber;
        PageSize = pageSize;
        OrderBy = orderBy;
    }
}

