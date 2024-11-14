using MediatR;
using Module.Shared.Models;
using SharedKernel.Dto.Features.School.Subject.Query;

namespace Module.Semester.Application.Features.Subject.Query;

public record GetAllSubjectsQuery() : IRequest<Result<IEnumerable<GetAllSubjectsResponse>>>;
