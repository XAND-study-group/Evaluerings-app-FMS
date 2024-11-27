namespace School.Domain.DomainServices.Interfaces;

public interface IUserDomainService
{
    bool DoesUserEmailExist(string email);
}