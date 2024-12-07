using Module.Feedback.Domain.Entities;

namespace Module.Feedback.Domain.WrapperObjects;

public class ClassId : Entity
{
    public Guid ClassIdValue { get; protected set; }

    protected ClassId() { }
    private ClassId(Guid classIdValue)
    {
        ClassIdValue = classIdValue;
    }

    public static ClassId Create(Guid id)
        => new(id);
}