﻿using MediatR;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.ExitSlip.Query;

public sealed record GetExitSlipWithQuestionsQuery(Guid exitSlipId) :
    IRequest<Result<IEnumerable<GetDetailedExitSlipResponse>?>>;