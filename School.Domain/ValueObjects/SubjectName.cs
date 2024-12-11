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

    public bool Equals(SubjectName? other) => other != null && Value == other.Value;

    public override bool Equals(object? obj) => obj is SubjectName name && Value == name.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static implicit operator string(SubjectName name) => name.Value;

    public static implicit operator SubjectName(string name) => new(name);
}