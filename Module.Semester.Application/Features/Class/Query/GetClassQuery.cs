using MediatR;
using Module.Semester.Application.Features.Class.Query.Dto;

namespace Module.Semester.Application.Features.Class.Query;

public record GetClassQuery(Guid SeminarId) : IRequest<GetClassResponse>;