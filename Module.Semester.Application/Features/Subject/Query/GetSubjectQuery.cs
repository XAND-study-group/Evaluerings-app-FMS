using MediatR;
using SharedKernel.Dto.Features.Subject.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Subject.Query;

public record GetSubjectQuery(GetSubjectRequest GetSubjectRequest) : IRequest<Result<GetSubjectResponse?>>;