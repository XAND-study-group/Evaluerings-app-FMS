using System.Data;

namespace SharedKernel.Interfaces.UOF;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
}