using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.DomainServices;
using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Interfaces;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.Interfaces.DomainServices.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Feedback.Command;

public record CreateFeedbackCommand(CreateFeedbackRequest CreateFeedbackRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateFeedbackCommandHandler(
    IFeedbackRepository feedbackRepository,
    IValidationServiceProxy iIValidationServiceProxy,
    IHashIdService hashIdService) : IRequestHandler<CreateFeedbackCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateFeedbackCommand, Result<bool>>.Handle(CreateFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createFeedbackRequest = request.CreateFeedbackRequest;
            var room = await feedbackRepository.GetRoomByIAsync(createFeedbackRequest.roomId);

            // Do
            var feedback = await room.AddFeedbackAsync(createFeedbackRequest.userId, createFeedbackRequest.title,
                createFeedbackRequest.problem, createFeedbackRequest.solution, hashIdService, iIValidationServiceProxy);

            // Save
            await feedbackRepository.CreateFeedbackAsync(feedback);

            return Result<bool>.Create("Evaluering oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}