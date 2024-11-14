﻿using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Module.User.Domain.DomainServices.Interfaces;

namespace Module.User.Domain.DomainServices;

public class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(User.Domain.Entities.User user)
    {
        var secretKey = configuration["Jwt:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.Firstname + " " + user.Lastname)
        ];
        
        claims.AddRange(user.AccountClaims.Select(claim => new Claim(claim.ClaimName, claim.ClaimValue)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:ExpirationInDays")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);
        return token;
    }
}