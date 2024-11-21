namespace School.Domain.ValueObjects;

public class ClassRoom
{
    public string Value { get; init; }

    public ClassRoom(string value)
    {
        Validate(value);
        Value = value;
    }

    private void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        
        if (value.Length > 50)
            throw new ArgumentException("Value cannot be longer than 50 characters.", nameof(value));
    }
    
    public static implicit operator string(ClassRoom classRoom) => classRoom.Value;
    public static implicit operator ClassRoom(string classRoom) => new(classRoom);
}