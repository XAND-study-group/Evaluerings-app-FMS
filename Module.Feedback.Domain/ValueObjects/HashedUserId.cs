using System.Security.Cryptography;

namespace Module.Feedback.Domain.ValueObjects;

public record HashedUserId
{
    private HashedUserId(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static HashedUserId Create(Guid value) => new(value);


    private static string Hash(Guid id)
    {
        // Convert GUID to byte array
        var guidBytes = id.ToByteArray();
        // Perform the SHA256 hashing algorithm on guidBytes
        var hashBytes = SHA256.HashData(guidBytes);
        // Convert the bytes to a hexadecimal string
        return Convert.ToHexString(hashBytes);
    }

    public override string ToString() => Value;

    public static implicit operator string(HashedUserId hashedUserId) => hashedUserId.Value;

    public static implicit operator HashedUserId(Guid value)
    {
        var hashedId = Hash(value);
        return new HashedUserId(hashedId);
    }
}