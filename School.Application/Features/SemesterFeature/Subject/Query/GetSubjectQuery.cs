using MediatR;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Subject.Query;

public record GetSubjectQuery(GetSubjectRequest GetSubjectRequest) : IRequest<Result<GetDetailedSubjectResponse?>>;