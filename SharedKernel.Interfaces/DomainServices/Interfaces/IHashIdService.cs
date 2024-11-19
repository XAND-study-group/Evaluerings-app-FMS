namespace SharedKernel.Interfaces.DomainServices.Interfaces;

public interface IHashIdService
{
    string Hash(Guid id);
    bool Verify(Guid requestId, string storedId);
}