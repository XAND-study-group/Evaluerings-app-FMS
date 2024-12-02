﻿namespace SharedKernel.Dto.Features.School.Class.Query;

public record GetClassUserResponse(
    Guid Id)
{
    public GetClassUserResponse() : this(Guid.Empty) { }
}