using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Cart.Commands.Base;

public abstract class CartCommandBase : IRequest<CartDomain>
{
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public bool Cancel { get; set; }
    public decimal TotalSold { get; set; }
    public List<CartProducCommand> Products { get; set; } = [];
    public List<CartWithCalculatorCommand> CartWithCalculators { get; set; } = [];


}

public class CartProducCommand
{
    public int ProductId { get; set; }
    public string? Title { get;  set; } 
    public decimal Price { get;  set; }
    public string? Description { get;  set; } 
    public string? Category { get;  set; } 
    public string? Image { get;  set; } 
    public ProductRatingCommand Rating { get;  set; }
    public int Quantity { get; set; }
    public string? Subsidiary { get; set; }
}

public class CartWithCalculatorCommand
{
    public decimal PriceWithDiscount { get; set; }
    public decimal Discount { get; set; }
    public string? DiscountMessage { get; set; }
}


public class ProductRatingCommand
{  
    public double Rate { get;  set; }
    public int Count { get;  set; }
}

