using MediatR;
using Module.Semester.Application.Features.Semester.Query.Dto;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemesterQuery(Guid SemesterId) : IRequest<GetSemesterResponse>;