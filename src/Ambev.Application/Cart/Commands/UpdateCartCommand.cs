using Ambev.Application.Cart.Commands.Base;

namespace Ambev.Application.Cart.Commands;

public sealed class UpdateCartCommand : CartCommandBase
{
    public int Id { get; set; }
}
