using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Product.Commands;

public class DeleteProductCommand : IRequest<ProductDomain>
{
    public int Id { get; set; }
}
