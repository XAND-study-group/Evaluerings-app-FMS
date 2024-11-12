using System.Text.RegularExpressions;
using Module.User.Domain.DomainServices.Interfaces;

namespace Module.User.Domain.Entities;

public class AccountLogin
{
    public Guid Id { get; }
    public string Email { get; protected set; }
    public string PasswordHash { get; protected set; }

    protected AccountLogin()
    {
        
    }
    
    private AccountLogin(string email, string password, IPasswordHasher passwordHasher)
    {
        AssurePasswordCompliesWithRequirements(password);

        Email = email;
        PasswordHash = passwordHasher.Hash(password);
    }

    public static AccountLogin Create(string email, string password, IPasswordHasher passwordHasher)
    {
        return new AccountLogin(email, password, passwordHasher);
    }

    protected void AssurePasswordCompliesWithRequirements(string password)
    {
        if (password.Length < 10)
            throw new ArgumentException("Adgangskode skal være minimum 10 karaktere langt");
        if (password.All(c => !char.IsUpper(c)))
            throw new ArgumentException("Adgangskode skal have mindst ét stort bogstav");
        if (password.All(c => !char.IsNumber(c)))
            throw new ArgumentException("Adgangskode skal have mindst ét tal");
        
        var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
        if (regexItem.IsMatch(password))
            throw new ArgumentException("Adgangskoden skal have mindst ét specialtegn");
    }
}