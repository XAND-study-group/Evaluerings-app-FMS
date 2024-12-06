using School.Domain.Entities;

namespace School.Domain.Test.Fakes.Semester;

public class FakeUser : Entities.User
{
    public FakeUser(Guid id, string role)
    {
        Id = id;
        AddAccountClaim(AccountClaim.Create("Role", role));
    }
}