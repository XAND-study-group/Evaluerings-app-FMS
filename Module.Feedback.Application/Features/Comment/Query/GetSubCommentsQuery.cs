using MediatR;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Comment.Query;

public record GetSubCommentsQuery(Guid CommentId) : IRequest<Result<IEnumerable<GetCommentResponse>?>>;

