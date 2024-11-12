﻿using MediatR;
using Module.Shared.Models;
using SharedKernel.Dto.Features.Class.Query;

namespace Module.Semester.Application.Features.Class.Query;

public record GetClassesByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<GetClassesResponse>>>;