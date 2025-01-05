namespace School.Domain.ValueObjects;

public sealed class UserFirstname : IEquatable<UserFirstname>
{
    private UserFirstname(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; init; }

    bool IEquatable<UserFirstname>.Equals(UserFirstname? other)
    {
        if (other is null)
            return false;

        // Case-Insensetive Comparison
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    private void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Firstname cannot be empty or whitespace", nameof(value));

        if (value.Length > 50)
            throw new ArgumentException("Firstname cannot exceed 50 characters", nameof(value));

        if (!value.All(char.IsLetter))
            throw new ArgumentException("Firstname can only contain letters", nameof(value));
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as UserFirstname);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(UserFirstname value)
    {
        return value.Value;
    }

    public static implicit operator UserFirstname(string value)
    {
        return new UserFirstname(value);
    }

    public static bool operator ==(UserFirstname left, UserFirstname right)
    {
        if (left is null)
            return right is null;
    
        return left.Equals(right);
    }
    
    public static bool operator !=(UserFirstname left, UserFirstname right)
    {
        return !(left == right);
    }
}