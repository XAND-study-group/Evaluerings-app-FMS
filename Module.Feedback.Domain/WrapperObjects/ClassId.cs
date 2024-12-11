using SharedKernel.Models;

namespace Module.Feedback.Domain.WrapperObjects;

public class ClassId : Entity
{
    protected ClassId()
    {
    }

    private ClassId(Guid classIdValue)
    {
        ClassIdValue = classIdValue;
    }

    public Guid ClassIdValue { get; protected set; }

    public static ClassId Create(Guid id)
    {
        return new ClassId(id);
    }
}