﻿namespace SharedKernel.Dto.Features.Evaluering.Answer.Command;

public record CreateAnswerRequest(
        Guid userId,
        Guid QuestionId,
        Guid ExitslipId,
        string Text);