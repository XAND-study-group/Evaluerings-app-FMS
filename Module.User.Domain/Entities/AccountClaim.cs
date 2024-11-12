namespace Module.User.Domain.Entities;

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
    
    public static AccountClaim Create(string claimName, string claimValue)
    {
        return new AccountClaim(claimName, claimValue);
    }
}