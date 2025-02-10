using Ambev.Domain.Constants;
using Ambev.Domain.Entities.Base;
using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using FluentValidation.Results;

namespace Ambev.Domain.Entities;

public class CartDomain : IEntity
{
    
    public CartDomain() { }

    private readonly CartValidator _cartValidator = new();

    public CartDomain(int id, int userId, DateTime date, bool cancel, ICollection<CartProductDomain>? products) 
    {

        Id = id;
        UserId = userId;
        Date = date;
        Cancel = cancel;
        Products = products;
        CartWithCalculators = Discount(products);

       
        if (!CartDomainResult().IsValid)
            throw new DomainValidationException(ResourceMessagesException.ERROR_DOMAIN, CartDomainResult().Errors);
    }
    public int Id { get; private set; } 
    public int UserId { get; private set; }
    public DateTime Date { get; private set; }
    public bool Cancel { get; private set; }
    public decimal TotalSold { get; private set; }
    public ICollection<CartProductDomain> Products { get; private set; } = [];
    public ICollection<CartWithCalculatorDomain> CartWithCalculators { get; private set; } = [];

    
    private List<CartWithCalculatorDomain> Discount(ICollection<CartProductDomain>? products)
    {
        if (products == null || !products.Any())
            return new List<CartWithCalculatorDomain>();

        List<CartWithCalculatorDomain> cartWithCalculators = new();

        var categoryQuantities = products
            .GroupBy(cp => cp.Category)
            .ToDictionary(g => g.Key, g => g.Sum(cp => cp.Quantity));

        foreach (var product in products)
        {
            if (product.Quantity <= 0) continue;

            var totalQuantity = categoryQuantities[product.Category];

            var (discountRule, message) = totalQuantity switch
            {
                < 4 => (DiscountRules.NoDiscount, ResourceMessagesException.QUANTITY_LESS_THAN_4),
                >= 4 and < 10 => (DiscountRules.DiscountFor4To9Items, ResourceMessagesException.QUANTITY_GREATER_THAN_4),
                >= 10 and <= 20 => (DiscountRules.DiscountFor10To20Items, ResourceMessagesException.QUANTITY_BETWEEN_10_AND_20),
                _ => (DiscountRules.NoDiscount, ResourceMessagesException.QUANTITY_GREATER_THAN_20)
            };

            cartWithCalculators.Add(new CartWithCalculatorDomain(product.Quantity, product.Price, discountRule, message));
        }

        TotalSold = cartWithCalculators.Sum(p => p.PriceWithDiscount);

        return cartWithCalculators;

    }

    public ValidationResult CartDomainResult()
    {
        return _cartValidator.Validate(this);
    }

    public object GetId() => Id;
   
}







