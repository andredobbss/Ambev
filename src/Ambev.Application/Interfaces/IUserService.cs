using Ambev.Application.Enums;
using Ambev.Domain.Entities;
using Ambev.Domain.Entities.Auth;

namespace Ambev.Application.Interfaces;

public interface IUserService
{
    Task<(IEnumerable<UserDomain> Data, int TotalItems, int TotalPages)> GetUsersByFilterToPagedListAsync(object filterValue, EUserFilterType filterType, int pageNumber, int pageSize, string orderBy = null);
    Task<UserDomain> GetUserByIdAsync(int id);
    Task<AuthenticationUserDomain> Login(string? username, string? password);
    Task<UserDomain> RegisterUserAsync(UserDomain userDomain);
    Task<UserDomain> UpdateUserAsync(UserDomain userDomain);
    Task<UserDomain?> DeleteUserAsync(int id);
}
