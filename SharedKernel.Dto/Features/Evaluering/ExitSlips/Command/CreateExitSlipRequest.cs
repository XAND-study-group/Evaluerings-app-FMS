﻿using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlips.Command;
public record CreateExitSlipRequest(
    Guid OwnerId,
    Guid LectureId,
    string Title,
    int MaxQuestionCount,
    ExitSlipActiveStatus ActiveStatus);
