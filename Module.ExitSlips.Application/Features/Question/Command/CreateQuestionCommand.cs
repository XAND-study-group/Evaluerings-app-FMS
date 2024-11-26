using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Question.Command;

public record CreateQuestionCommand(Guid ExitSlipId, string Text) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Result<bool>>
{
    private readonly IExitSlipRepository _exitSlipRepository;

    public CreateQuestionCommandHandler(IExitSlipRepository exitSlipRepository)
    {
        _exitSlipRepository = exitSlipRepository;
    }

    public async Task<Result<bool>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var exitSlip = await _exitSlipRepository.GetExitSlipByIdAsync(request.ExitSlipId);
            if (exitSlip == null)
            {
                return Result<bool>.Create("ExitSlip not found", false, ResultStatus.Error);
            }

            // Do
            exitSlip.AddQuestion(request.Text);

            // Save
            // await _exitSlipRepository.UpdateExitSlipAsync(exitSlip);

            return Result<bool>.Create("Question created", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}
