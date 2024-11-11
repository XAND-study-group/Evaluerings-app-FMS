using Module.Semester.Domain.Entities;

namespace Module.Semester.Domain.Test.Fakes;

public class FakeUser : User
{
    public FakeUser(Guid id)
    {
        Id = id;
    }
}