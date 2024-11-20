using System.Security.Cryptography;
using School.Domain.DomainServices.Interfaces;

namespace School.Domain.DomainServices;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int PepperSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
    
    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var pepper = RandomNumberGenerator.GetBytes(PepperSize);
        var hashWithSalt = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        var hashWithPepper = Rfc2898DeriveBytes.Pbkdf2(hashWithSalt, pepper, Iterations, Algorithm, HashSize);
        
        return $"{Convert.ToHexString(hashWithPepper)}-{Convert.ToHexString(salt)}-{Convert.ToHexString(pepper)}";
    }

    public bool Verify(string requestPassword, string accountPasswordHash)
    {
        var parts = accountPasswordHash.Split('-');
        
        var hash = Convert.FromHexString(parts[0]);
        var salt = Convert.FromHexString(parts[1]);
        var pepper = Convert.FromHexString(parts[2]);

        var inputHashWithSalt = Rfc2898DeriveBytes.Pbkdf2(requestPassword, salt, Iterations, Algorithm, HashSize);
        var inputHashWithPepper = Rfc2898DeriveBytes.Pbkdf2(inputHashWithSalt, pepper, Iterations, Algorithm, HashSize);

        // return hash.SequenceEqual(inputHash); // Attackers can see how long it takes to compare and then find the correct hash
        return CryptographicOperations.FixedTimeEquals(hash, inputHashWithPepper);
    }
}