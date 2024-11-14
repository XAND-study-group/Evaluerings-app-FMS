using MediatR;
using SharedKernel.Dto.Features.Class.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Class.Query;

public record GetClassQuery(Guid SeminarId) : IRequest<Result<GetClassResponse?>>;