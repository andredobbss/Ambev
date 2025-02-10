using Ambev.Domain.Entities;
using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Implementations;
using Ambev.Test.Repositories.Interfaces;
using Bogus;
using FluentAssertions;

namespace Ambev.Test.Tests;

public class ProductDomainTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldCreateValidProducts()
    {
        //Arrange
        IProducts products = new Products();

        //Act
        var productsDomain = products.ProdcutsFake(10);

        // Assert
        Assert.NotNull(productsDomain);
        Assert.NotEmpty(productsDomain.First().Title);
        Assert.IsType<decimal>(productsDomain.First().Price);
        Assert.NotEmpty(productsDomain.First().Description);
        Assert.NotEmpty(productsDomain.First().Category);
        Assert.NotEmpty(productsDomain.First().Image);
    }

    [Fact]
    public void MustHaveErrorWhenUserTitleIsNull()
    {
        //Arrange
        IProducts products = new Products();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 2);
        var description = _faker.Commerce.ProductDescription();
        var category = _faker.Commerce.Categories(1).First();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();


        // Act
        Action act = () => new ProductDomain(id, null, price, description, category, image, new ProductRatingDomain(_faker.Random.Double(1, 5), _faker.Random.Int(1, 1000)));

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN) // Mensagem principal
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.NULL_FIELD);
    }

    [Fact]
    public void ThereMustBeAnErrorWhenThePriceHasMoreThanTwoDecimalPlaces()
    {
        //Arrange
        IProducts products = new Products();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 7);
        var description = _faker.Commerce.ProductDescription();
        var category = _faker.Commerce.Categories(1).First();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();


        // Act
        Action act = () => new ProductDomain(id, title, price, description, category, image, new ProductRatingDomain(_faker.Random.Double(1, 5), _faker.Random.Int(1, 1000)));

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN) // Mensagem principal
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.INVALID_NUMBER);
    }

    [Fact]
    public void MustHaveErrorWhenUserDescriptionIsNull()
    {
        //Arrange
        IProducts products = new Products();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 7);
        var description = _faker.Commerce.ProductDescription();
        var category = _faker.Commerce.Categories(1).First();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();


        // Act
        Action act = () => new ProductDomain(id, title, price, null, category, image, new ProductRatingDomain(_faker.Random.Double(1, 5), _faker.Random.Int(1, 1000)));

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN) // Mensagem principal
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.NULL_FIELD);
    }

    [Fact]
    public void MustHaveErrorWhenUserCategoryIsNull()
    {
        //Arrange
        IProducts products = new Products();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 7);
        var description = _faker.Commerce.ProductDescription();
        var category = _faker.Commerce.Categories(1).First();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();


        // Act
        Action act = () => new ProductDomain(id, title, price, description, null, image, new ProductRatingDomain(_faker.Random.Double(1, 5), _faker.Random.Int(1, 1000)));

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN) // Mensagem principal
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.NULL_FIELD);
    }

    [Fact]
    public void MustHaveErrorWhenUserImageIsNull()
    {
        //Arrange
        IProducts products = new Products();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 7);
        var description = _faker.Commerce.ProductDescription();
        var category = _faker.Commerce.Categories(1).First();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();


        // Act
        Action act = () => new ProductDomain(id, title, price, description, category, null, new ProductRatingDomain(_faker.Random.Double(1, 5), _faker.Random.Int(1, 1000)));

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN) // Mensagem principal
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.NULL_FIELD);
    }

    [Fact]
    public void ThereShouldBeAnErrorWhenThePriceIsZero()
    {
        //Arrange
        IProducts products = new Products();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 7);
        var description = _faker.Commerce.ProductDescription();
        var category = _faker.Commerce.Categories(1).First();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();


        // Act
        Action act = () => new ProductDomain(id, title, 0, description, category, image, new ProductRatingDomain(_faker.Random.Double(1, 5), _faker.Random.Int(1, 1000)));

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN) // Mensagem principal
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.NUMBER_GREATER_THAN_ZERO);
    }
}
