using Ambev.Domain.Entities;
using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Implementations;
using Ambev.Test.Repositories.Interfaces;
using Bogus;
using FluentAssertions;

namespace Ambev.Test.Tests;

public class CartDomainTests
{
    private readonly Faker _faker = new();



    [Fact]
    public void ShouldCreateValidCart()
    {
        // Arrange
        IProducts products = new Products();
        ICartProduct cartProduct = new CartProduct();
        ICart cart = new Cart();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();


        // Act
        var productsDomain = products.ProdcutsFake(10);

        var cartProductDomain = cartProduct.CartProductFake(productsDomain, 5);

        var cartDomain = cart.CartFake(cartProductDomain, userId, date, false, 2);


        // Assert
        Assert.NotNull(cartDomain);
        Assert.Equal(userId, cartDomain.First().UserId);
        Assert.Equal(date, cartDomain.First().Date);
        Assert.False(cartDomain.First().Cancel);
        Assert.NotEmpty(cartDomain.First().Products);
        Assert.NotEmpty(cartDomain.First().CartWithCalculators);
        Assert.True(cartDomain.First().TotalSold > 0);
    }


    [Fact]
    public void ShouldHaveNoDiscountForPurchasesOfLessThanFourItems()
    {
        // Arrange
        ICart cart = new Cart();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 2);
        var description = _faker.Commerce.ProductDescription();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();

        var product = new List<ProductDomain>();

        for (var i = 0; i < 3; i++)
        {
            var item = new ProductDomain(id, title, price, description, "fashion", image, new ProductRatingDomain(10, 200));
            product.Add(item);
        }

        var listCartDomain = new List<CartProductDomain>();

        foreach (var item in product)
        {
            var cartsProduct = new CartProductDomain(item, 1, subsidiary);
            listCartDomain.Add(cartsProduct);
        }


        //Act
        var cartDomain = cart.CartFake(listCartDomain, userId, date, false, 1);


        // Assert
        foreach (var cartItem in cartDomain)
        {
            cartItem.CartWithCalculators.Count.Should().Be(3);
            cartItem.TotalSold.Should().BeGreaterThan(0);
            cartItem.CartWithCalculators.Any(c => c.DiscountMessage == ResourceMessagesException.QUANTITY_LESS_THAN_4).Should().BeTrue();
            cartItem.CartWithCalculators.Any(c => c.Discount == 0).Should().BeTrue();
        }
    }

    [Fact]
    public void ThereMustBeaTenPercentDiscountForPurchasesOfFourOrMoreIdenticalItems()
    {
        // Arrange
        ICart cart = new Cart();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 2);
        var description = _faker.Commerce.ProductDescription();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();

        var product = new List<ProductDomain>();

        for (var i = 0; i < 7; i++)
        {
            var item = new ProductDomain(id, title, price, description, "fashion", image, new ProductRatingDomain(10, 200));
            product.Add(item);
        }

        var listCartDomain = new List<CartProductDomain>();

        foreach (var item in product)
        {
            var cartsProduct = new CartProductDomain(item, 1, subsidiary);
            listCartDomain.Add(cartsProduct);
        }


        //Act
        var cartDomain = cart.CartFake(listCartDomain, userId, date, false, 1);


        // Assert
        foreach (var cartItem in cartDomain)
        {
            cartItem.CartWithCalculators.Count.Should().Be(7);
            cartItem.TotalSold.Should().BeGreaterThan(0);
            cartItem.CartWithCalculators.Any(c => c.DiscountMessage == ResourceMessagesException.QUANTITY_GREATER_THAN_4).Should().BeTrue();
            cartItem.CartWithCalculators.Any(c => c.Discount == 0.1m).Should().BeTrue();
        }
    }

    [Fact]
    public void ThereMustBeaTwentyPercentDiscountForPurchasesBetweenTenAndTwentyIdenticalItems()
    {
        // Arrange
        ICart cart = new Cart();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 2);
        var description = _faker.Commerce.ProductDescription();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();

        var product = new List<ProductDomain>();

        for (var i = 0; i < 20; i++)
        {
            var item = new ProductDomain(id, title, price, description, "fashion", image, new ProductRatingDomain(10, 200));
            product.Add(item);
        }

        var listCartDomain = new List<CartProductDomain>();

        foreach (var item in product)
        {
            var cartsProduct = new CartProductDomain(item, 1, subsidiary);
            listCartDomain.Add(cartsProduct);
        }


        //Act
        var cartDomain = cart.CartFake(listCartDomain, userId, date, false, 1);


        // Assert
        foreach (var cartItem in cartDomain)
        {
            cartItem.CartWithCalculators.Count.Should().Be(20);
            cartItem.TotalSold.Should().BeGreaterThan(0);
            cartItem.CartWithCalculators.Any(c => c.DiscountMessage == ResourceMessagesException.QUANTITY_BETWEEN_10_AND_20).Should().BeTrue();
            cartItem.CartWithCalculators.Any(c => c.Discount == 0.2m).Should().BeTrue();
        }

    }

    [Fact]
    public void YouMustNotSellMoreThanTwentyIdenticalItems()
    {
        // Arrange
        ICart cart = new Cart();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 2);
        var description = _faker.Commerce.ProductDescription();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();

        var product = new List<ProductDomain>();

        for (var i = 0; i < 25; i++)
        {
            var item = new ProductDomain(id, title, price, description, "fashion", image, new ProductRatingDomain(10, 200));
            product.Add(item);
        }

        var listCartDomain = new List<CartProductDomain>();

        foreach (var item in product)
        {
            var cartsProduct = new CartProductDomain(item, 1, subsidiary);
            listCartDomain.Add(cartsProduct);
        }


        //Act
        var cartDomain = cart.CartFake(listCartDomain, userId, date, false, 1);


        // Assert
        foreach (var cartItem in cartDomain)
        {
            cartItem.CartWithCalculators.Count.Should().Be(25);
            cartItem.TotalSold.Should().BeGreaterThan(0);
            cartItem.CartWithCalculators.Any(c => c.DiscountMessage == ResourceMessagesException.QUANTITY_GREATER_THAN_20).Should().BeTrue();
            cartItem.CartWithCalculators.Any(c => c.Discount == 0).Should().BeTrue();
        }
    }

   
    [Fact]
    public void MustHaveErrorWhenUserIdIsZero()
    {
        // Arrange
        ICart cart = new Cart();

        var userId = _faker.Random.Int(1, 1000);
        var date = _faker.Date.Past();
        var id = _faker.Random.Int(1, 1000);
        var title = _faker.Commerce.Product();
        var price = Math.Round(_faker.Random.Decimal(1, 1000), 2);
        var description = _faker.Commerce.ProductDescription();
        var category = _faker.Commerce.Categories(1).First();
        var image = _faker.Image.PicsumUrl();
        var subsidiary = _faker.Company.CompanyName();

        var product = new List<ProductDomain>();

        for (var i = 0; i < 5; i++)
        {
            var item = new ProductDomain(id, title, price, description, category, image, new ProductRatingDomain(10, 200));
            product.Add(item);
        }

        var listCartDomain = new List<CartProductDomain>();

        foreach (var item in product)
        {
            var cartsProduct = new CartProductDomain(item, 1, subsidiary);
            listCartDomain.Add(cartsProduct);
        }

        // Act
        Action act = () => new CartDomain(id, 0, date, true, listCartDomain);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN) // Mensagem principal
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.INVALID_IDENTIFIED);
    }

}
