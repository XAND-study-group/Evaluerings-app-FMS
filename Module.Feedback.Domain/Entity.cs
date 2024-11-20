namespace Module.Feedback.Domain;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public byte[] RowVersion { get; protected set; }
}