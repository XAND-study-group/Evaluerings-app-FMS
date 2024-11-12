using System.Text.RegularExpressions;
using Module.User.Domain.DomainServices.Interfaces;

namespace Module.User.Domain.Entities;

public class AccountLogin : Entity
{
    public string Email { get; protected set; }
    public string PasswordHash { get; protected set; }
    public User User { get; set; }

    protected AccountLogin()
    {
        
    }
    
    private AccountLogin(string email, string password, User user, IPasswordHasher passwordHasher)
    {
        AssurePasswordCompliesWithRequirements(password);

        Email = email;
        PasswordHash = passwordHasher.Hash(password);
    }

    public static AccountLogin Create(string email, string password, User user, IPasswordHasher passwordHasher)
    {
        return new AccountLogin(email, password, user, passwordHasher);
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