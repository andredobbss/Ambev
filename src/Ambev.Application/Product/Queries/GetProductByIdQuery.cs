using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Product.Queries;

public class GetProductByIdQuery : IRequest<ProductDomain>
{
    public int Id { get; set; }
}
