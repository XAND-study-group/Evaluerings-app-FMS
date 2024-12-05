namespace School.Domain.ValueObjects;

public class RefreshToken
{
    public string? Token { get; init; }
    public DateTime? ExpirationDate { get; init; }
    
    private RefreshToken(string? token, DateTime? expirationDate)
    {
        AssureDateIsInFuture(expirationDate);
        Token = token;
        ExpirationDate = expirationDate;
    }

    public static RefreshToken Create(string? token, DateTime? expirationDate)
        => new RefreshToken(token, expirationDate);
    
    private void AssureDateIsInFuture(DateTime? value)
    {
        if (value is null)
            return;
        
        if (value < DateTime.Now)
            throw new ArgumentException("Refresh tokens udløbsdato skal være i fremtiden", nameof(value));
    }
}