using Module.Authentication.Domain.Entity;

namespace Module.Authentication.Domain.DomainServices.Interfaces;

public interface ITokenProvider
{
    string Create(Account account);
}