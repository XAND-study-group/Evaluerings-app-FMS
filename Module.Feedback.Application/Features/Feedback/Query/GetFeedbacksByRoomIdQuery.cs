using MediatR;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Feedback.Query;

public record GetFeedbacksByRoomIdQuery(GetFeedbacksByRoomIdRequest GetFeedbacksByRoomIdRequest) : IRequest<Result<IEnumerable<GetAllFeedbacksResponse>?>>;