using MediatR;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Semester.Query;

public record GetSemesterQuery(Guid SemesterId) : IRequest<Result<GetDetailedSemesterResponse?>>;