using Ambev.Application.Enums;
using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.User.Queries;

public class GetUserByFilterQuery : IRequest<(IEnumerable<UserDomain> Data, int TotalItems, int TotalPages)>
{
    public GetUserByFilterQuery(object filterValue, EUserFilterType filterType, int pageNumber, int pageSize, string orderBy = null)
    {
        FilterValue = filterValue;
        FilterType = filterType;
        PageNumber = pageNumber;
        PageSize = pageSize;
        OrderBy = orderBy;
    }

    public object FilterValue { get; }
    public EUserFilterType FilterType { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public string OrderBy { get; }
}
