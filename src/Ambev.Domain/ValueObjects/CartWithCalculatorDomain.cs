namespace Ambev.Domain.ValueObjects;

public sealed class CartWithCalculatorDomain
{
    protected CartWithCalculatorDomain() { }
 
    public CartWithCalculatorDomain(int quantity, decimal price, decimal discount, string? discountMessage)
    {

        PriceWithDiscount = CalculateTotalPrice(quantity, price, discount);
        Discount = discount;
        DiscountMessage = discountMessage;
    }

    public decimal PriceWithDiscount { get; private set; }
    public decimal Discount { get; private set; }
    public string? DiscountMessage { get; private set; }

    private static decimal CalculateTotalPrice(int quantity, decimal price, decimal discount)
    {
        return quantity * price * (1 - discount);
    }
}
