using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.User.Queries;

public class GetUserByIdQuery : IRequest<UserDomain>
{
    public int Id { get; set; }
}
