using Ambev.Application.Enums;
using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Entities.Auth;
using Ambev.Domain.IUnitOfWork;
using Ambev.Domain.Resourcers;
using System.Linq.Expressions;

namespace Ambev.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

   
    public async Task<(IEnumerable<UserDomain> Data, int TotalItems, int TotalPages)> GetUsersByFilterToPagedListAsync(object filterValue, EUserFilterType filterType, int pageNumber, int pageSize, string orderBy = null)
    {
        Expression<Func<UserDomain, bool>> filterExpression = u => true;

        switch (filterType)
        {
            case EUserFilterType.Email when filterValue is string emailValue:
                filterExpression = u => u.Email == emailValue;
                break;          

            case EUserFilterType.UserName when filterValue is string usernameValue:
                filterExpression = u => u.Username == usernameValue;
                break;

            default:
                throw new KeyNotFoundException(ResourceMessagesException.INVALID_FILTER);
        }

        var users = await _unitOfWork.userRepository.GetToPagedListAsync(filterExpression, pageNumber, pageSize, orderBy);

        return (users);
    }

    public async Task<UserDomain> GetUserByIdAsync(int id)
    {
        var user = await _unitOfWork.userRepository.GetSingleAsync(u => u.Id == id);

        return user;
    }

    public async Task<AuthenticationUserDomain> Login(string? username, string? password)
    {
        if (username is null || password is null)
            throw new KeyNotFoundException(ResourceMessagesException.ERROR_LOGIN);

        return await _unitOfWork.userRepository.Login(username, password);
    }

    public async Task<UserDomain> RegisterUserAsync(UserDomain userDomain)
    {
       await  _unitOfWork.userRepository.Add(userDomain);

        await _unitOfWork.Commit();

        return userDomain;
    }

    public async Task<UserDomain> UpdateUserAsync(UserDomain userDomain)
    {

        var existingUser = await _unitOfWork.userRepository.GetSingleAsync(u => u.Id == userDomain.Id);
        if (existingUser is null)
            throw new KeyNotFoundException($"{ResourceMessagesException.ERROR_ID_NEW_EXCEPTION} {userDomain.Id}");

        await _unitOfWork.userRepository.Update(existingUser);

        await _unitOfWork.Commit();

        return userDomain;
    }

    public async Task<UserDomain?> DeleteUserAsync(int id)
    {
        var userDomainResult = await _unitOfWork.userRepository.GetSingleAsync(u => u.Id == id);

        if (userDomainResult is null)
            throw new KeyNotFoundException($"{ResourceMessagesException.ERROR_ID_NEW_EXCEPTION} {id}");

        await _unitOfWork.userRepository.Delete(userDomainResult);

        await _unitOfWork.Commit();

        return userDomainResult;
    }

}

