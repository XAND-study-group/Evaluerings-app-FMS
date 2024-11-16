namespace SharedKernel.ValueObjects;

public class RefreshToken
{
    public string? Token { get; init; }
    public DateTime? ExpirationDate { get; init; }
}