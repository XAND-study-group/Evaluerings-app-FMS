namespace Module.User.Domain.Entities;

public class AccountClaim
{
    public Guid Id { get; set; }
    public Module.User.Domain.Entities.User User { get; set; }
    public string ClaimName { get; set; }
    public string ClaimValue { get; set; }
}