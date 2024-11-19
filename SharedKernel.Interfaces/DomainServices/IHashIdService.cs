namespace SharedKernel.Interfaces.DomainServices;

public interface IHashIdService
{
    string Hash(Guid id);
}