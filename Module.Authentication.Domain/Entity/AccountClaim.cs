namespace Module.Authentication.Domain.Entity;

public class AccountClaim
{
    public Guid Id { get; set; }
    public Account Account { get; set; }
    public string ClaimName { get; set; }
    public string ClaimValue { get; set; }
}