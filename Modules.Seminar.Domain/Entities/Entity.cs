namespace Module.Semester.Domain.Entities;

public abstract class Entity : IEquatable<Entity>
{
    // Database Properties
    public Guid Id { get; protected set; }
    public byte[] RowVersion { get; protected set; }
    
    public bool Equals(Entity? other)
    {
        return other?.Id == Id;
    }
}