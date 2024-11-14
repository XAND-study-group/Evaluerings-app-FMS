namespace SharedKernel.ValueObjects;

public record Title
{
    public string Value { get; init; }

    public Title(string value)
    {
        Validate(value);
        Value = value;
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
}