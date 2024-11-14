using MediatR;
using Module.Shared.Models;
using SharedKernel.Dto.Features.Subject.Query;

namespace Module.Semester.Application.Features.Subject.Query;

public record GetSubjectsByClassQuery(GetSubjectsByClassRequest Request) : IRequest<Result<IEnumerable<GetAllSubjectsResponse>?>>;