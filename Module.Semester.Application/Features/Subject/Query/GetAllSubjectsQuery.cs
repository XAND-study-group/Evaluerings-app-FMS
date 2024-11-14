using MediatR;
using Module.Shared.Models;
using SharedKernel.Dto.Features.Subject.Command;
using SharedKernel.Dto.Features.Subject.Query;

namespace Module.Semester.Application.Features.Subject.Query;

public record GetAllSubjectsQuery() : IRequest<Result<IEnumerable<GetAllSubjectsResponse>>>;
