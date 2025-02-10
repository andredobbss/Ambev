using Ambev.Domain.Entities;
using Ambev.Domain.ValueObjects;
using Bogus;
using NSubstitute;
using FluentValidationResults = FluentValidation.Results.ValidationResult;


namespace Ambev.Test;


public class CartProductDomainTests
{
    //private readonly Faker _faker = new();

    //[Fact]
    //public void CartProductDomain_Should_Calculate_TotalPrice_Correctly()
    //{
    //    // Arrange
    //    var product = new ProductDomain(
    //        title: _faker.Commerce.ProductName(),
    //        price: 100,
    //        description: _faker.Commerce.ProductDescription(),
    //        category: "Eletrônicos",
    //        image: _faker.Image.PicsumUrl(),
    //        rating: new ProductRatingDomain(4.5, 100),
    //        Id: 1
    //    );
    //    int quantity = 5;
    //    decimal discount = 0.10m; // 10%
    //    var expectedTotal = quantity * product.Price * (1 - discount);

    //    // Act
    //    var cartProduct = new CartProductDomain(product, quantity, discount);

    //    // Assert
    //    Assert.Equal(expectedTotal, cartProduct.TotalPriceItems);
    //}

    //[Theory]
    //[InlineData(2, 100, 0.2, 160)] // 20% de desconto
    //[InlineData(1, 50, 0, 50)] // Sem desconto
    //[InlineData(10, 200, 0.15, 1700)] // 15% de desconto
    //public void CartProductDomain_Should_Apply_Discount_Correctly(int quantity, decimal price, decimal discount, decimal expectedTotal)
    //{
    //    // Arrange
    //    var product = new ProductDomain(
    //        title: "Teste",
    //        price: price,
    //        description: "Descrição",
    //        category: "Eletrônicos",
    //        image: "image.jpg",
    //        rating: new ProductRatingDomain(4.5, 100),
    //        Id: 1
    //    );

    //    // Act
    //    var cartProduct = new CartProductDomain(product, quantity, discount);

    //    // Assert
    //    Assert.Equal(expectedTotal, cartProduct.TotalPriceItems);
    //}

    //[Fact]
    //public void CartProductDomain_Should_Validate_Product_Correctly()
    //{
    //    // Arrange
    //    var product = Substitute.For<ProductDomain>("Test Product", 50, "Desc", "Cat", "img.jpg", new ProductRatingDomain(5, 10), 1);
    //    var cartProduct = new CartProductDomain(product, 2, 0.1m);

    //    // Act
    //    FluentValidationResults result = cartProduct.CartProductResult();

    //    // Assert
    //    Assert.True(result.IsValid);
    //}
}