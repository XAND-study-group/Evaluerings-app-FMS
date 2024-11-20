namespace Module.User.Domain.DomainServices.Interfaces;

public interface ITokenProvider
{
    string GenerateAccessToken(User.Domain.Entities.User user);
    string GenerateRefreshToken();
    string GenerateRandomCode();
}