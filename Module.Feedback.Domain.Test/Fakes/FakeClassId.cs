using Module.Feedback.Domain.WrapperObjects;

namespace Module.Feedback.Domain.Test.Fakes;

public class FakeClassId : ClassId
{
    public FakeClassId(Guid id)
    {
        ClassIdValue = id;
    }
}