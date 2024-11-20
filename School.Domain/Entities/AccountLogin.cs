using System.Text.RegularExpressions;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Enums.Features.Authentication;
using SharedKernel.Models;

namespace School.Domain.Entities;

public class AccountLogin : Entity
{
    #region Properties

    public string Email { get; protected set; }
    public string PasswordHash { get; protected set; }
    public User User { get; protected set; }

    public Role Role { get; protected set; }

    #endregion


    #region Constructors

    protected AccountLogin()
    {
        
    }
    
    private AccountLogin(string email, string password, User user, Role role, IPasswordHasher passwordHasher)
    {
        AssurePasswordCompliesWithRequirements(password);

        Email = email;
        PasswordHash = passwordHasher.Hash(password);
        User = user;
        Role = role;
    }

    #endregion


    #region FactoryPattern

    public static AccountLogin Create(string email, string password, User user, Role role, IPasswordHasher passwordHasher)
    {
        return new AccountLogin(email, password, user, role, passwordHasher);
    }

    public void Update(string email, string password, User user, IPasswordHasher passwordHasher)
    {
        Email = email;
        PasswordHash = passwordHasher.Hash(password);
        User = user;
    }

    public void ChangePassword(string newPassword, IPasswordHasher passwordHasher)
    {
        AssurePasswordCompliesWithRequirements(newPassword);
        PasswordHash = passwordHasher.Hash(newPassword);
    }

    #endregion


    #region RequirementMethods

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

    #endregion
}