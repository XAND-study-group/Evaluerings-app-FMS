using System.Text.RegularExpressions;

namespace School.Domain.ValueObjects;

public sealed class UserEmail : IEquatable<UserEmail>
{
    private UserEmail()
    {
    }

    private UserEmail(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; init; }


    // this is in case if we wanna hide a part of the email 
    public string LocalPart => Value.Substring(0, Value.IndexOf('@'));
    public string DomainPart => Value.Substring(Value.IndexOf('@') + 1);

    bool IEquatable<UserEmail>.Equals(UserEmail? other)
    {
        if (other is null)
            return false;

        return Value == other.Value;
    }

    public static UserEmail Create(string value) => new(value);

    public static async Task<UserEmail> CreateAsync(string value) => new(value);

    private void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty or whitespace", nameof(value));

        if (value.Length > 254)
            throw new ArgumentException("Email cannot exceed 254 characters", nameof(value));

        if (!IsValidEmail(value))
            throw new ArgumentException("Email format is invalid", nameof(value));


        var normalizedEmail = NormalizeEmail(value);
    }

    private bool IsValidEmail(string value)
    {
        const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
    }

    private string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

    public override bool Equals(object? obj) => Equals(obj is UserEmail);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static implicit operator string(UserEmail value) => value.Value;

    public static bool operator ==(UserEmail left, UserEmail right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    public static bool operator !=(UserEmail left, UserEmail right) => !(left == right);
}