using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.User.Commands.Base;

public abstract class UserCommandBase : IRequest<UserDomain>
{
    public string? Email { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public NameUserCommand? Name { get; set; }
    public AddressUserCommand? Address { get; set; }
    public EUserStatusCommand Status { get; set; }
    public EUserRoleCommand Role { get; set; }
    public ExternalIdentityCommand? ExternalIdentity { get; set; }
}

public sealed class NameUserCommand
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public sealed class AddressUserCommand
{
    public string? City { get; set; }
    public string? Street { get; set; }
    public int Number { get; set; }
    public string? ZipCode { get; set; }
    public string? Phone { get; set; }
    public GeolocationUserCommand? Geolocation { get; set; }
}

public sealed class ExternalIdentityCommand
{
    public string? Provider { get; set; }
    public string? ExternalId { get; set; }
}

public sealed class GeolocationUserCommand
{
    public string? Lat { get; set; }
    public string? Long { get; set; }
}

public enum EUserRoleCommand
{
    Customer,
    Manager,
    Admin
}

public enum EUserStatusCommand
{
    Active,
    Inactive,
    Suspended
}
