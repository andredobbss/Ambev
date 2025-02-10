using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Cart.Commands;

public sealed class DeleteCartCommand : IRequest<CartDomain>
{
    public int Id { get; set; }
}
