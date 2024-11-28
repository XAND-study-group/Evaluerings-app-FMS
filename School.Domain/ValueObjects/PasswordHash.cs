using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace School.Domain.ValueObjects;

public class PasswordHash
{
    private const int SaltSize = 16;
    private const int PepperSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;
    
    public string Value { get; init; }
    
    public PasswordHash(string value)
    {
        Validate(value);
        Value = Hash(value);
    }

    private void Validate(string value)
    {
        if (value.Length < 10)
            throw new ArgumentException("Adgangskode skal være minimum 10 karaktere langt");
        if (value.All(c => !char.IsUpper(c)))
            throw new ArgumentException("Adgangskode skal have mindst ét stort bogstav");
        if (value.All(c => !char.IsNumber(c)))
            throw new ArgumentException("Adgangskode skal have mindst ét tal");

        var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
        if (regexItem.IsMatch(value))
            throw new ArgumentException("Adgangskoden skal have mindst ét specialtegn");
    }
    
    private string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var pepper = RandomNumberGenerator.GetBytes(PepperSize);
        var hashWithSalt = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _algorithm, HashSize);
        var hashWithPepper = Rfc2898DeriveBytes.Pbkdf2(hashWithSalt, pepper, Iterations, _algorithm, HashSize);
        
        return $"{Convert.ToHexString(hashWithPepper)}-{Convert.ToHexString(salt)}-{Convert.ToHexString(pepper)}";
    }

    public bool Verify(string requestPassword)
    {
        var parts = Value.Split('-');
        
        var hash = Convert.FromHexString(parts[0]);
        var salt = Convert.FromHexString(parts[1]);
        var pepper = Convert.FromHexString(parts[2]);

        var inputHashWithSalt = Rfc2898DeriveBytes.Pbkdf2(requestPassword, salt, Iterations, _algorithm, HashSize);
        var inputHashWithPepper = Rfc2898DeriveBytes.Pbkdf2(inputHashWithSalt, pepper, Iterations, _algorithm, HashSize);

        // return hash.SequenceEqual(inputHash); // Attackers can see how long it takes to compare and then find the correct hash
        return CryptographicOperations.FixedTimeEquals(hash, inputHashWithPepper);
    }
    
    public static implicit operator string(PasswordHash value) => value.Value;
    public static implicit operator PasswordHash(string value) => new(value);
}