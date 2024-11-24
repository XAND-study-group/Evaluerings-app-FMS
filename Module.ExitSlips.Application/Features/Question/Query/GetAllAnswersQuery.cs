using MediatR;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Question.Query;

public record GetAllAnswersQuery(Guid QuestionId) : IRequest(Result<IEnumerable<GetAllAnswersResponse>>);