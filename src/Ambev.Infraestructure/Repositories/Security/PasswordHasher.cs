using Ambev.Domain.Constants;
using Ambev.Domain.Repositories.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

#pragma warning disable SYSLIB0023

namespace Ambev.Infraestructure.Repositories.Security;

public class PasswordHasher : IPasswordHasher
{
    public bool IsHash(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;

        if (Regex.IsMatch(input, "^[a-fA-F0-9]{64}$")) return true;

        if (Regex.IsMatch(input, "^[a-fA-F0-9]{32}$")) return true;

        if (Regex.IsMatch(input, "^[a-fA-F0-9]{40}$")) return true;

        if (Regex.IsMatch(input, @"^\$2[aby]\$\d{2}\$.{53}$")) return true;

        if (Regex.IsMatch(input, @"^[A-Za-z0-9+/=]{43,}$")) return true;

        return false;
    }

    

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            throw new ArgumentException("O hashedPassword está vazio ou nulo.");
        }

        byte[] hashBytes;
        try
        {
            hashBytes = Convert.FromBase64String(hashedPassword);
        }
        catch (FormatException)
        {
            throw new FormatException("O hashedPassword não está em um formato Base64 válido.");
        }

        if (hashBytes.Length < HashComponents.SaltSize + HashComponents.KeySize)
        {
            throw new ArgumentException("O hashedPassword possui um tamanho inválido.");
        }

        byte[] salt = new byte[HashComponents.SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, HashComponents.SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashComponents.Iterations, HashAlgorithmName.SHA256);
        byte[] key = pbkdf2.GetBytes(HashComponents.KeySize);

        if (key.Length != HashComponents.KeySize)
        {
            throw new ArgumentException("A chave derivada (key) não tem o tamanho correto.");
        }

        for (int i = 0; i < HashComponents.KeySize; i++)
        {
            if (hashBytes[HashComponents.SaltSize + i] != key[i])
                return false;
        }

        return true;
    }

}

#pragma warning restore SYSLIB0023