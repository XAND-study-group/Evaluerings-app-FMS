using MediatR;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Class.Query;

public record GetClassQuery(Guid SeminarId) : IRequest<Result<GetDetailedClassResponse?>>;