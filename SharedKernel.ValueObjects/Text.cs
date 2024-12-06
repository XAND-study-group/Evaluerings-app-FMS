namespace SharedKernel.ValueObjects;

public record Text
{
    public Text(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; init; }

    private void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

        if (value.Length > 500)
            throw new ArgumentException("Value cannot be longer than 500 characters.", nameof(value));
    }

    public static implicit operator Text(string value)
    {
        return new Text(value);
    }

    public static implicit operator string(Text value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}