namespace SharedKernel.ValueObjects;

public record Title
{
    public Title(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; init; }


    public virtual bool Equals(Title? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    private void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be null or whitespace.", nameof(value));

        if (value.Length > 100)
            throw new ArgumentException("Title cannot be longer than 100 characters.", nameof(value));
    }

    public static implicit operator string(Title title) => title.Value;

    public static implicit operator Title(string value) => new(value);

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    }

    public override string ToString()
    {
        return Value;
    }
}