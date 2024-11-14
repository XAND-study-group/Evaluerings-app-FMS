using MediatR;
using Module.Shared.Models;
using SharedKernel.Dto.Features.School.Semester.Query;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemesterQuery(Guid SemesterId) : IRequest<Result<GetSemesterResponse?>>;