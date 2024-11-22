using Module.Feedback.Domain.DomainServices.Interfaces;

namespace Module.Feedback.Domain.ValueObjects;

public record HashedId
{
    public string Value { get; init; }

    private HashedId(string value)
    {
        Value = value;
    }
    
    public static HashedId Create(Guid value, IHashIdService hashIdService)
    {
        var hashedValue = hashIdService.Hash(value);
        return new HashedId(hashedValue);
    }
    
    public static implicit operator string(HashedId hashedId) => hashedId.Value;
}