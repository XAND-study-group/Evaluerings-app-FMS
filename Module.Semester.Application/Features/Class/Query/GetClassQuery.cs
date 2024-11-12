using MediatR;
using Module.Shared.Models;
using SharedKernel.Dto.Features.Class.Query;

namespace Module.Semester.Application.Features.Class.Query;

public record GetClassQuery(Guid SeminarId) : IRequest<Result<GetClassResponse?>>;