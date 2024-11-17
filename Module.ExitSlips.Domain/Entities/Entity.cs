namespace Module.ExitSlip.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; protected set; }
        public byte[] RowVersion { get; protected set; }

        bool IEquatable<Entity>.Equals(Entity? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Entity);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }
}
