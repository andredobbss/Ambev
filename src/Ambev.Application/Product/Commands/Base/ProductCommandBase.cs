using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Product.Commands.Base;

public abstract class ProductCommandBase : IRequest<ProductDomain>
{
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Image { get; set; }
    public ProductRatingCommand? Rating { get; set; }
}

public class ProductRatingCommand
{
    public double Rate { get; set; }
    public int Count { get; set; }
}

