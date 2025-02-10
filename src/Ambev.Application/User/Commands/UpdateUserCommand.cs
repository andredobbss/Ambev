using Ambev.Application.User.Commands.Base;

namespace Ambev.Application.User.Commands;

public sealed class UpdateUserCommand : UserCommandBase
{
    public int Id { get; set; }
   
}

