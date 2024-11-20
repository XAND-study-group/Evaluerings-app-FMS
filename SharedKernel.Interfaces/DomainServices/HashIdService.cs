using System.Security.Cryptography;
using SharedKernel.Interfaces.DomainServices.Interfaces;

namespace SharedKernel.Interfaces.DomainServices;

public class HashIdService : IHashIdService
{
    string IHashIdService.Hash(Guid id)
    {
        // Convert GUID to byte array
        byte[] guidBytes = id.ToByteArray();
        // Perform the SHA256 hashing algorithm on guidBytes
        byte[] hashBytes = SHA256.HashData(guidBytes);
        // Convert the bytes to a hexadecimal string
        return $"{Convert.ToHexString(hashBytes)}";
    }

    bool IHashIdService.Verify(Guid requestId, string storedId)
    {
        // Convert storedId to a byte array
        var storedHash = Convert.FromHexString(storedId);

        // Perform same actions as the Hash method: Convert ID to byte[] and Hash the byte[]
        var requestGuidBytes = requestId.ToByteArray();
        var requestHashBytes = SHA256.HashData(requestGuidBytes);

        // Compare the two hashed byte[] and return the result.
        return CryptographicOperations.FixedTimeEquals(requestHashBytes, storedHash);
    }
}