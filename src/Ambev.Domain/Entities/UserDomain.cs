using Ambev.Domain.Constants;
using Ambev.Domain.Entities.Base;
using Ambev.Domain.Enums;
using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using FluentValidation.Results;
using System.Security.Cryptography;


#pragma warning disable SYSLIB0023

namespace Ambev.Domain.Entities;

public sealed class UserDomain : IEntity
{
    protected UserDomain() { }

    private readonly UserValidator _userValidator = new();

    public UserDomain(int id, string email, string username, string password, NameDomain name, AddressDomain address, EUserStatusDomain status, EUserRoleDomain role, ExternalIdentityDomain externalIdentity)
    {
       
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        Name = name;
        Address = address;
        Status = status;
        Role = role;
        ExternalIdentity = externalIdentity;

        if (!UserDomainResult().IsValid)
            throw new DomainValidationException(ResourceMessagesException.ERROR_DOMAIN, UserDomainResult().Errors);

        Password = HashPassword(Password);

    }

    public int Id { get; set; }
    public string Email { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Password { get; private set; } = null!;
    public NameDomain Name { get; private set; }
    public AddressDomain Address { get; private set; }
    public EUserStatusDomain Status { get; private set; }
    public EUserRoleDomain Role { get; private set; }
    public ExternalIdentityDomain ExternalIdentity { get; private set; }

    public ValidationResult UserDomainResult()
    {
        return _userValidator.Validate(this);
    }

    public string HashPassword(string password)
    {
        using var rng = new RNGCryptoServiceProvider();
        byte[] salt = new byte[HashComponents.SaltSize];
        rng.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashComponents.Iterations, HashAlgorithmName.SHA256);
        byte[] key = pbkdf2.GetBytes(HashComponents.KeySize);

        byte[] hashBytes = new byte[HashComponents.SaltSize + HashComponents.KeySize];
        Array.Copy(salt, 0, hashBytes, 0, HashComponents.SaltSize);
        Array.Copy(key, 0, hashBytes, HashComponents.SaltSize, HashComponents.KeySize);

        return Convert.ToBase64String(hashBytes);
    }

    public object GetId() => Id;
}

#pragma warning restore SYSLIB0023