namespace School.Domain.ValueObjects;

public sealed class SubjectName : IEquatable<SubjectName>
{
    public SubjectName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));

        Value = value;
    }

    public string Value { get; init; }

    public bool Equals(SubjectName? other)
    {
        return other != null && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is SubjectName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(SubjectName name)
    {
        return name.Value;
    }

    public static implicit operator SubjectName(string name)
    {
        return new SubjectName(name);
    }
}