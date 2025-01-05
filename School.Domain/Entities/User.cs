using System.Text.RegularExpressions;
using School.Domain.DomainServices.Interfaces;
using School.Domain.ValueObjects;
using SharedKernel.Enums.Features.Authentication;
using SharedKernel.Models;

namespace School.Domain.Entities;

public class User : Entity
{
    public void AddAccountClaim(AccountClaim claim)
    {
        if (AccountClaims.Contains(claim))
            throw new ArgumentException("Brugeren har allerede denne rettighed");

        _accountClaims.Add(claim);
    }

    public void AddRefreshToken(string token, int days)
    {
        _refreshTokens.Add(RefreshToken.Create(token, DateTime.Now.AddYears(days)));
    }

    public void RemoveRefreshToken(string token)
    {
        _refreshTokens.RemoveAll(t => t.Token == token);
    }

    public void ResetRefreshToken()
    {
        _refreshTokens.Clear();
    }

    public RefreshToken TryGetRefreshToken(string token)
    {
        return _refreshTokens.FirstOrDefault(t => t.Token == token) ??
               throw new ArgumentException("Refresh token kunne ikke findes");
    }

    public void ChangePassword(string newPassword)
    {
        AssurePasswordCompliesWithRequirements(newPassword);
        PasswordHash = newPassword;
    }

    #region Properties

    public UserFirstname Firstname { get; protected set; }
    public UserLastname Lastname { get; protected set; }
    public UserEmail Email { get; protected set; }

    public PasswordHash PasswordHash { get; protected set; }
    public Role UserRole { get; protected set; }

    public IEnumerable<Semester> Semesters { get; protected set; }
    private readonly List<AccountClaim> _accountClaims = [];
    public IReadOnlyCollection<AccountClaim> AccountClaims => _accountClaims;
    protected List<RefreshToken> _refreshTokens = [];
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;

    #endregion

    #region Constructors

    protected User()
    {
    }

    private User(string fristname, string lastname, string email, string password, Role role)
    {
        AssurePasswordCompliesWithRequirements(password);

        Firstname = fristname;
        Lastname = lastname;
        Email = UserEmail.Create(email);
        PasswordHash = password;
        UserRole = role;
    }

    #endregion

    #region User Methodes

    public static User Create(string firstname, string lastname, string email, string password, Role role,
        IEnumerable<User> otherUsers)
    {
        return new User(firstname, lastname, email, password, role);
    }

    public static User Create(string firstname, string lastname, string email, string password,
        Role role,
        IUserDomainService userDomainService,
        IAccountClaimRepository accountClaimRepository)
    {
        if (userDomainService.DoesUserEmailExist(email))
            throw new ArgumentException($"A User with email '{email}' already exists.");

        var user = new User(firstname, lastname, email, password, role);
        accountClaimRepository.AddClaimsForRole(user, role);
        return user;
    }

    #endregion

    #region User BusinessLogic Methodes

    protected void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Firstname cannot be empty or null.",
                nameof(name)); // her er jeg ikke helt sikker på hvorfor der skal tilføjes "nameof"

        if (name.Length > 100)
            throw new ArgumentException("Firstname cannot exceed 50 characters.", nameof(name));
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

    #endregion
}