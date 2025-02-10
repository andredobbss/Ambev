using Ambev.Domain.Entities;
using Ambev.Domain.Entities.Auth;
using Ambev.Domain.Repositories.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ambev.Infraestructure.Repositories.Auth;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IConfiguration _configuration;

    public AuthenticationRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthenticationUserDomain GenerateJwtToken(UserDomain user)
    {
        #region define delarações do usuário (Claims)

        var claims = new List<Claim>
        {
             new(JwtRegisteredClaimNames.Sub, user.Username),
             new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new("UserId", user.Id.ToString()),
        };

        #endregion

        #region gera uma chave com base no algoritmo simétrico

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KeY"]));

        #endregion

        #region gera a assinatura digital do token usando o algoritmo Hmac e a chave privada

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        #endregion

        #region tempo de expiração do Token    

        var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["TokenConfiguration:ExpireHours"]));

        #endregion

        #region classe que representa o JWT e gera o Token

        JwtSecurityToken token = new JwtSecurityToken(
                         audience: _configuration["TokenConfiguration:Audience"],
                         issuer: _configuration["TokenConfiguration:Issuer"],
                         claims: claims,
                         expires: expiration,
                         signingCredentials: credentials);

        #endregion

        #region Retorna o Token

        return new AuthenticationUserDomain()
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = "Transação efetuada com sucesso!"
        };

        #endregion
    }
}
