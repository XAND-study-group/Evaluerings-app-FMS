﻿namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetCommentResponse(
    Guid Id,
    Guid UserId,
    string CommentText,
    DateTime Created,
    byte[] RowVersion);