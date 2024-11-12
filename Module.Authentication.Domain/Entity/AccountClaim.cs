namespace Module.Authentication.Domain.Entity;

public class AccountClaim
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public string ClaimName { get; set; }
    public string ClaimValue { get; set; }
}