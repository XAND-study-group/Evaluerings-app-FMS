﻿using MediatR;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemestersByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<GetSimpleSemesterResponse>>>;