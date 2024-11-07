namespace Module.Authentication.Domain.DomainServices.Interfaces;

public interface IPasswordHasher
{
    public string Hash(string password);
    bool Verify(string requestPassword, string? accountPassword);
}