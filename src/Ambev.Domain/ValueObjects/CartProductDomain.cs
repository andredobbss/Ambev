using Ambev.Domain.Entities;

namespace Ambev.Domain.ValueObjects;

public sealed class CartProductDomain
{ 
    protected CartProductDomain() { }

    public CartProductDomain(ProductDomain product, int quantity, string subsidiary)
    {
        Title = product.Title;
        ProductId = product.Id;
        Quantity = quantity;
        Price = product.Price;
        Category = product.Category;
        Subsidiary = subsidiary;
       
    }

    public string Title { get; private set; } = string.Empty;
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string Subsidiary { get; private set; }

}


