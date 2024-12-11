using SharedKernel.Models;

namespace School.Domain.Entities;

public class AccountClaim : Entity
{
    public string ClaimName { get; protected set; }
    public string ClaimValue { get; protected set; }
    
    protected AccountClaim()
    {
    }

    private AccountClaim(string claimName, string claimValue)
    {
        ClaimName = claimName;
        ClaimValue = claimValue;
    }

    #region FactoryPattern

    public static AccountClaim Create(string claimName, string claimValue) 
        => new(claimName, claimValue);

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