using SharedKernel.Interfaces.DomainServices;

namespace SharedKernel.ValueObjects;

public record HashedId
{
    public string Value { get; set; }

    public HashedId(Guid value, IHashIdService hashIdService)
    {
        Value = hashIdService.Hash(value);
    }
    
    public static implicit operator string(HashedId hashedId) => hashedId.Value;
}