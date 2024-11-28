using SharedKernel.Dto.Features.Evaluering.Room.Query;

namespace Evaluation.Web.Services.Interfaces;

public interface IEvaluationProxy
{
    Task<IEnumerable<GetSimpleRoomResponse>> GetRoomsByClassIdAsync(Guid classId);
    Task<IEnumerable<GetSimpleRoomResponse>> GetAllRoomsAsync();
}