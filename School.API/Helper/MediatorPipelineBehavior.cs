using MediatR;
using SharedKernel.Interfaces;
using SharedKernel.Interfaces.UOF;

namespace School.API.Helper;

public class MediatorPipelineBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly Type commandMarkerInterface = typeof(ITransactionalCommand);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var isTransactionalCommand = commandMarkerInterface.IsAssignableFrom(typeof(TRequest));

        try
        {
            if (isTransactionalCommand)
                await unitOfWork.BeginTransactionAsync();

            var response = await next();

            if (isTransactionalCommand)
                await unitOfWork.CommitAsync();

            return response;
        }
        catch (Exception)
        {
            if (isTransactionalCommand)
                await unitOfWork.RollbackAsync();

            throw;
        }
    }
}