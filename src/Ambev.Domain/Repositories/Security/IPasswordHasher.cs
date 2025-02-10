namespace Ambev.Domain.Repositories.Security;

public interface IPasswordHasher
{
    bool VerifyPassword(string password, string hashedPassword);
    bool IsHash(string input);
}
