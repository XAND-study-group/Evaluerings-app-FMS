using School.Domain.Entities;

namespace School.Domain.DomainServices.Interfaces;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    string GenerateRandomCode();
}