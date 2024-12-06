using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Feedback.Command;

public record ChangeFeedbackStatusCommand(ChangeFeedbackStatusRequest ChangeFeedbackStatusRequest)
    : IRequest<Result<bool>>;

public class ChangeFeedbackStatusCommandHandler(IFeedbackRepository feedbackRepository)
    : IRequestHandler<ChangeFeedbackStatusCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ChangeFeedbackStatusCommand request, CancellationToken cancellationToken)
    {
        // Load
        var changeFeedbackStatusReuquest = request.ChangeFeedbackStatusRequest;
        var feedback = await feedbackRepository.GetFeedbackByIdAsync(changeFeedbackStatusReuquest.FeedbackId);

        // Do
        feedback.ChangeStatus(changeFeedbackStatusReuquest.State);

        // Save
        await feedbackRepository.UpdateFeedbackAsync(feedback, changeFeedbackStatusReuquest.RowVersion);

        return Result<bool>.Create("State for feedback opdateret", true, ResultStatus.Updated);
    }
}