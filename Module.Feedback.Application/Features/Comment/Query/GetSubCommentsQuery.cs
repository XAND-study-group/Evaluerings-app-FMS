using MediatR;
using SharedKernel.Dto.Features.Evaluering.Comment.Query;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Comment.Query;

public record GetSubCommentsQuery(GetSubCommentsRequest GetSubCommentsRequest) : IRequest<Result<IEnumerable<GetCommentResponse>?>>;

