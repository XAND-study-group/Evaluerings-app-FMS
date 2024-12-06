using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Feedback.Command;

public record CreateFeedbackCommand(CreateFeedbackRequest CreateFeedbackRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateFeedbackCommandHandler(
    IFeedbackRepository feedbackRepository,
    IValidationServiceProxy iIValidationServiceProxy) : IRequestHandler<CreateFeedbackCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateFeedbackCommand, Result<bool>>.Handle(CreateFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createFeedbackRequest = request.CreateFeedbackRequest;
            var room = await feedbackRepository.GetRoomByIAsync(createFeedbackRequest.RoomId);

            // Do
            var feedback = await Domain.Entities.Feedback.CreateAsync(createFeedbackRequest.UserId,
                createFeedbackRequest.Title,
                createFeedbackRequest.Problem, createFeedbackRequest.Solution, room, iIValidationServiceProxy);

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