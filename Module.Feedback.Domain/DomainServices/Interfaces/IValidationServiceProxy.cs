﻿using SharedKernel.Dto.Features.Evaluering.Proxy;

namespace Module.Feedback.Domain.DomainServices.Interfaces;

public interface IValidationServiceProxy
{
    Task<GeminiResponse> IsAcceptableContentAsync(string problem, string solution, string title);
    Task<GeminiResponse> IsAcceptableCommentAsync(string title);
}