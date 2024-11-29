using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Evaluation.Web.Services.Interfaces;

public interface IEvaluationProxy
{
    Task<IEnumerable<GetSimpleRoomResponse>> GetRoomsByClassIdAsync(Guid classId);
    Task<IEnumerable<GetSimpleRoomResponse>> GetAllRoomsAsync();
    Task<Result<bool>> CreateFeedbackAsync(CreateFeedbackRequest request);
}