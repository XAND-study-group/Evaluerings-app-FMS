namespace Module.Feedback.Domain;

public class Entity
{
    public Guid Id { get; protected set; }
    public byte[] RowVersion { get; protected set; }
}