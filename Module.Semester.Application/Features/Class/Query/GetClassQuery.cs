using MediatR;
using SharedKernel.Dto.Features.Class.Query;

namespace Module.Semester.Application.Features.Class.Query;

public record GetClassQuery(Guid SeminarId) : IRequest<GetClassResponse>;