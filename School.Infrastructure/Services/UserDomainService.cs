using School.Domain.DomainServices.Interfaces;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Services;

public class UserDomainService(SchoolDbContext dbContext) : IUserDomainService
{
    bool IUserDomainService.DoesUserEmailExist(string email)
        => dbContext.Users.AsEnumerable().Any(user => user.Email == email);
}