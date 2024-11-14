namespace Module.User.Domain.Entities;

public class AccountClaim : Entity
{
    #region Properties

    public string ClaimName { get; protected set; }
    public string ClaimValue { get; protected set; }

    #endregion


    #region Constructors

    protected AccountClaim()
    {
        
    }
    
    private AccountClaim(string claimName, string claimValue)
    {
        ClaimName = claimName;
        ClaimValue = claimValue;
    }

    #endregion


    #region FactoryPattern

    public static AccountClaim Create(string claimName, string claimValue)
    {
        return new AccountClaim(claimName, claimValue);
    }

    public void Update(string claimName, string claimValue)
    {
        ClaimName = claimName;
        ClaimValue = claimValue;
    }

    public void ChangeValue(string claimValue)
    {
        ClaimValue = claimValue;
    }

    #endregion
    
}