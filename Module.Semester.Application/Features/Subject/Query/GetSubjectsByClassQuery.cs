using MediatR;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Subject.Query;

public record GetSubjectsByClassQuery(GetSubjectsByClassRequest Request) : IRequest<Result<IEnumerable<GetDetailedSubjectResponse>?>>;