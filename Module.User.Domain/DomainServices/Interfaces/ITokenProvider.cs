namespace Module.User.Domain.DomainServices.Interfaces;

public interface ITokenProvider
{
    string Create(User.Domain.Entities.User user);
}