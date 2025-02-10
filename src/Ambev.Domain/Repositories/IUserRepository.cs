using Ambev.Domain.Entities;
using Ambev.Domain.Entities.Auth;
using Ambev.Domain.Repositories.Base;

namespace Ambev.Domain.Repositories;

public interface IUserRepository : IRepository<UserDomain>
{
    Task<AuthenticationUserDomain> Login(string Username, string Password);
}
