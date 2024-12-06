namespace School.Domain.ValueObjects;

public class RefreshToken
{
    private RefreshToken(string? token, DateTime? expirationDate)
    {
        AssureDateIsInFuture(expirationDate);
        Token = token;
        ExpirationDate = expirationDate;
    }

    public string? Token { get; init; }
    public DateTime? ExpirationDate { get; init; }

    public static RefreshToken Create(string? token, DateTime? expirationDate)
    {
        return new RefreshToken(token, expirationDate);
    }

    private void AssureDateIsInFuture(DateTime? value)
    {
        if (value is null)
            return;

        if (value < DateTime.Now)
            throw new ArgumentException("Refresh tokens udløbsdato skal være i fremtiden", nameof(value));
    }
}