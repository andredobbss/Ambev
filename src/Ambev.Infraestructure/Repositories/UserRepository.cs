using Ambev.Domain.Constants;
using Ambev.Domain.Entities;
using Ambev.Domain.Entities.Auth;
using Ambev.Domain.Repositories;
using Ambev.Domain.Repositories.Auth;
using Ambev.Domain.Resourcers;
using Ambev.Infraestructure.Database;
using Ambev.Infraestructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace Ambev.Infraestructure.Repositories;

public class UserRepository : Repository<UserDomain>, IUserRepository
{
    private readonly AppDbContext _appDbContext;

    private readonly IAuthenticationRepository _authentication;

    private readonly IMongoCollection<UserDomain> _collection;



    public UserRepository(IMongoDatabase database, AppDbContext appDbContext, IAuthenticationRepository authentication, IMongoCollection<UserDomain> collection) : base(appDbContext, authentication, collection)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication), "IAuthenticationRepository não foi resolvido.");
        _collection = collection ?? throw new ArgumentNullException(nameof(collection));
    }

    public async Task<AuthenticationUserDomain> Login(string Username, string Password)
    {
        var user = await _collection.Find(u => u.Username == Username).FirstOrDefaultAsync();

        if (user != null)
        {
            var checkPassword = VerifyPassword(Password, user.Password);

            if (checkPassword)
                return _authentication.GenerateJwtToken(user);

            throw new KeyNotFoundException(ResourceMessagesException.INVALID_LOGIN);
        }
        throw new KeyNotFoundException(ResourceMessagesException.INVALID_LOGIN);

    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[HashComponents.SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, HashComponents.SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashComponents.Iterations, HashAlgorithmName.SHA256);
        byte[] key = pbkdf2.GetBytes(HashComponents.KeySize);

        for (int i = 0; i < HashComponents.KeySize; i++)
        {
            if (hashBytes[HashComponents.SaltSize + i] != key[i])
                return false;
        }

        return true;
    }
}
