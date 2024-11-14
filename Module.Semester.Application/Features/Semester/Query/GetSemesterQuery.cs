using MediatR;
using SharedKernel.Dto.Features.Semester.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemesterQuery(Guid SemesterId) : IRequest<Result<GetSemesterResponse?>>;