using MediatR;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Dto.Features.Evaluering.Vote.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Vote.Query;

public record GetVotesByFeedbackIdQuery(GetVotesByFeedbackIdRequest GetVotesByFeedbackIdRequest) : IRequest<Result<IEnumerable<GetVoteResponse>>>;