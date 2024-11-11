using MediatR;
using SharedKernel.Dto.Features.Semester.Query;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemesterQuery(Guid SemesterId) : IRequest<GetSemesterResponse>;