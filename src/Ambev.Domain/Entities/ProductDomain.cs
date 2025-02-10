using Ambev.Domain.Entities.Base;
using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using FluentValidation.Results;

namespace Ambev.Domain.Entities;


public sealed class ProductDomain : IEntity
{
    protected ProductDomain() { }

    private readonly ProductValidator _productValidator = new();

    public ProductDomain(int id, string title, decimal price, string description, string category, string image, ProductRatingDomain rating)
    {
        Id = id;
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Rating = rating;

        if (!ProductDomainResult().IsValid)
            throw new DomainValidationException(ResourceMessagesException.ERROR_DOMAIN, ProductDomainResult().Errors);

    }

    public int Id { get; set; }
    public string Title { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public ProductRatingDomain Rating { get; private set; }

    public ValidationResult ProductDomainResult()
    {
        return _productValidator.Validate(this);
    }

    public object GetId() => Id;
}
