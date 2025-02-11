using Ambev.Domain.Entities;
using Ambev.Domain.Enums;
using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using Ambev.Test.Repositories.Implementations;
using Ambev.Test.Repositories.Interfaces;
using Bogus;
using FluentAssertions;

namespace Ambev.Test.Tests;

public class UserDomainTests
{

    private readonly Faker _faker = new();

    [Fact]
    public void ShouldCreateValidUsers()
    {
        // Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();

        // Act
        var userDomain = user.UserFake(id, email, userNane, "Teste@123", name, address, status, role, externalIdentity, 10);


        // Assert
        Assert.NotNull(userDomain);
        Assert.Equal(id, userDomain.First().Id);
        Assert.Equal(email, userDomain.First().Email);
        Assert.Equal(status, userDomain.First().Status);
        Assert.Equal(role, userDomain.First().Role);


        Assert.NotNull(userDomain.First().Name);
        Assert.Equal(name.FirstName, userDomain.First().Name.FirstName);
        Assert.Equal(name.LastName, userDomain.First().Name.LastName);


        Assert.NotNull(userDomain.First().Address);
        Assert.Equal(address.Street, userDomain.First().Address.Street);
        Assert.Equal(address.City, userDomain.First().Address.City);
        Assert.Equal(address.Number, userDomain.First().Address.Number);
        Assert.Equal(address.Phone, userDomain.First().Address.Phone);
        Assert.Equal(address.ZipCode, userDomain.First().Address.ZipCode);

        Assert.NotNull(userDomain.First().ExternalIdentity);
        Assert.Equal(externalIdentity.Provider, userDomain.First().ExternalIdentity.Provider);
        Assert.Equal(externalIdentity.ExternalId, userDomain.First().ExternalIdentity.ExternalId);
    }

    [Fact]
    public void MustHaveErrorWhenUserEmailIsNull()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, null, userNane, "Teste@123", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.NULL_FIELD);
    }

    [Fact]
    public void MustHaveErrorWhenUserEmailIsInvalid()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, "teste.com", userNane, "Teste@123", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.INVALID_EMAIL);
    }

    [Fact]
    public void MustHaveErrorWhenUserNameIsNull()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, email, null, "Teste@123", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.NULL_FIELD);
    }

    [Fact]
    public void ThereShouldBeAnErrorWhenThePasswordIsLessThanEightCharacters()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, email, userNane, "Test", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.PASSWORD_MIN_LENGTH);
    }

    [Fact]
    public void ThereShouldBeAnErrorWhenThePasswordIsLongerThanTwentyCharacters()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, email, userNane, "Test563247898797979798797987987987989", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.PASSWORD_MAX_LENGTH);
    }

    [Fact]
    public void ThereShouldBeAnErrorWhenThePasswordDoesNotContainAtLeastOneCapitalLetter()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, email, userNane, "test@test.com", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.PASSWORD_UPPERCASE_CHARACTERS);
    }

    [Fact]
    public void ThereShouldBeAnErrorWhenThePasswordDoesNotContainAtLeastOneLowercaseLetter()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, email, userNane, "TEST@TEST.COM", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.PASSWORD_LOWERCASE_CHARACTERS);
    }

    [Fact]
    public void ThereShouldBeAnErrorWhenThePasswordDoesNotContainAtLeastOneNumber()
    {
        //Arrange
        IUser user = new User();
        INameFaker nameFake = new NameFaker();
        IAddressFaker addressFake = new AddressFaker();
        IExternalIdentity externalIdentityFake = new ExternalIdentity();

        var id = _faker.Random.Int(1, 1000);
        var email = _faker.Person.Email;
        var userNane = _faker.Person.FirstName;
        var status = _faker.PickRandom<EUserStatusDomain>();
        var role = _faker.PickRandom<EUserRoleDomain>();
        var name = nameFake.NameFake();
        var address = addressFake.AddressFake();
        var externalIdentity = externalIdentityFake.ExternalIdentityFake();


        // Act
        Action act = () => new UserDomain(id, email, userNane, "Tes@test.com", name, address, status, role, externalIdentity);

        // Assert
        var exception = act.Should().Throw<DomainValidationException>()
            .WithMessage(ResourceMessagesException.ERROR_DOMAIN)
            .Which;

        exception.Errors.Should().Contain(error => error.ErrorMessage == ResourceMessagesException.PASSWORD_NUMBER);
    }
}
