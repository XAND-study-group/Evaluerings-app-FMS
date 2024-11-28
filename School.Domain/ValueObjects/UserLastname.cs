namespace School.Domain.ValueObjects
{
    public sealed class UserLastname : IEquatable<UserLastname>
    {
        public string Value { get; init; }

        private UserLastname(string value)
        {
            Validate(value);
            Value = value;
        }


        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Lastname cannot be empty or whitespace", nameof(value));

            if (value.Length > 50)
                throw new ArgumentException("Lastname cannot exceed 50 characters", nameof(value));

            if (!value.All(char.IsLetter))
                throw new ArgumentException("Lastname can only contain letters", nameof(value));
        }

        bool IEquatable<UserLastname>.Equals(UserLastname? other)
        {
            if (other is null)
                return false;

            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UserLastname);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }
        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(UserLastname value)
            => value.Value;
        public static implicit operator UserLastname(string value)
            => new(value);

        public static bool operator ==(UserLastname left, UserLastname right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(UserLastname left, UserLastname right)
        {
            return !(left == right);
        }

    }
}
