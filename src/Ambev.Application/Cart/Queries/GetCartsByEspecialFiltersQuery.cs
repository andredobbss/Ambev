using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Cart.Queries;

public class GetCartsByEspecialFiltersQuery : IRequest<IEnumerable<CartDomain>>
{
    public Dictionary<string, string> Filters { get; }

    public GetCartsByEspecialFiltersQuery(Dictionary<string, string> filters)
    {
        Filters = filters ?? new Dictionary<string, string>();
    }
}
