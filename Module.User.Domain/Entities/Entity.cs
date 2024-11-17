namespace Module.User.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        // Database Properties
        public Guid Id { get; protected set; }
        public byte[] RowVersion { get; protected set; }


        bool IEquatable<Entity>.Equals(Entity? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }


        public override bool Equals(object? obj)
        {
            return Equals(obj as Entity);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

    }
}