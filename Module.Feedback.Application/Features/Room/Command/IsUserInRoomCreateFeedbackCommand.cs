using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record IsUserInRoomCreateFeedbackCommand(
    Guid RoomId,
    IEnumerable<Guid> UserClasses) : IRequest<Result<bool>>;

public class IsUserInRoomCreateFeedbackCommandHandler(IRoomRepository roomRepository) : IRequestHandler<IsUserInRoomCreateFeedbackCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<IsUserInRoomCreateFeedbackCommand, Result<bool>>.Handle(IsUserInRoomCreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetRoomByIdAsync(request.RoomId);
        
        return Result<bool>.Create(
            "Nothing To Report",
            room.ClassIds.Any(classId => request.UserClasses.Any(userClassId => classId.ClassIdValue == userClassId)), 
            ResultStatus.Success);
    }
}