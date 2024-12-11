using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record IsUserInRoomUpdateFeedbackCommand(
    Guid FeedbackId,
    IEnumerable<Guid> UserClasses) : IRequest<Result<bool>>;
    
    public class IsUserInRoomUpdateFeedbackCommandHandler(IFeedbackRepository feedbackRepository) : IRequestHandler<IsUserInRoomUpdateFeedbackCommand, Result<bool>>
    {
        async Task<Result<bool>> IRequestHandler<IsUserInRoomUpdateFeedbackCommand, Result<bool>>.Handle(IsUserInRoomUpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await feedbackRepository.GetFeedbackByIdAsync(request.FeedbackId);

            return Result<bool>.Create(
                "Nothing To Report", 
                feedback.Room.ClassIds.Any(classId => request.UserClasses.Any(userClassId => classId.ClassIdValue == userClassId)), 
                ResultStatus.Success);
        }
    }