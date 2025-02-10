using Ambev.Application.Product.Commands.Base;

namespace Ambev.Application.Product.Commands;

public sealed class UpdateProductCommand : ProductCommandBase
{
    public int Id { get; set; }

}
