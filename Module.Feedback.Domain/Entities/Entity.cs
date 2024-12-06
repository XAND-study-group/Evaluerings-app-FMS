namespace Module.Feedback.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public byte[] RowVersion { get; protected set; }
}