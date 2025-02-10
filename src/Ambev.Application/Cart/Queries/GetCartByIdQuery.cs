using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Cart.Queries;

public class GetCartByIdQuery : IRequest<CartDomain>
{
    public int Id { get; set; }
}
