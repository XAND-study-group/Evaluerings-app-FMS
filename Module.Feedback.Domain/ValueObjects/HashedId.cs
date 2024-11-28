using System.Security.Cryptography;

namespace Module.Feedback.Domain.ValueObjects;

public record HashedId
{
    public string Value { get; init; }

    private HashedId(Guid value)
    {
        Value = Hash(value);
    }
    
    public static HashedId Create(Guid value)
    => new(value);
    
    
    private string Hash(Guid id)
    {
        // Convert GUID to byte array
        byte[] guidBytes = id.ToByteArray();
        // Perform the SHA256 hashing algorithm on guidBytes
        byte[] hashBytes = SHA256.HashData(guidBytes);
        // Convert the bytes to a hexadecimal string
        return Convert.ToHexString(hashBytes);
    }
    
    private bool Verify(Guid requestId, string storedId)
    {
        // Convert storedId to a byte array
        var storedHash = Convert.FromHexString(storedId);

        // Perform same actions as the Hash method: Convert ID to byte[] and Hash the byte[]
        var requestGuidBytes = requestId.ToByteArray();
        var requestHashBytes = SHA256.HashData(requestGuidBytes);

        // Compare the two hashed byte[] and return the result.
        return CryptographicOperations.FixedTimeEquals(requestHashBytes, storedHash);
    }
    
    public static implicit operator string(HashedId hashedId) => hashedId.Value;
    public static implicit operator HashedId(Guid value) => new(value);
}