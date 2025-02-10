using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Product.Queries;

public class GetProductsByEspcialFilterQuery : IRequest<IEnumerable<ProductDomain>>
{
    public Dictionary<string, string> Filters { get; }

    public GetProductsByEspcialFilterQuery(Dictionary<string, string> filters)
    {
        Filters = filters ?? new Dictionary<string, string>();
    }
}
