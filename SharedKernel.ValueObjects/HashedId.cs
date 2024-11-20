using SharedKernel.Interfaces.DomainServices;
using SharedKernel.Interfaces.DomainServices.Interfaces;

namespace SharedKernel.ValueObjects;

public record HashedId
{
    public string Value { get; set; }

    private HashedId(string hashedValue)
    {
        Value = hashedValue;
    }

    public static HashedId Create(Guid value, IHashIdService hashIdService)
    {
        var hashedValue = hashIdService.Hash(value);
        return new HashedId(hashedValue);
    }
    
    public static implicit operator string(HashedId hashedId) => hashedId.Value;
}