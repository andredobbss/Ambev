using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.User.Commands;

public class DeleteUserCommand : IRequest<UserDomain>
{
    public int Id { get; set; }
}
