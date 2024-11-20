using SharedKernel.Dto.Features.Evaluering.Proxy;

namespace Module.Feedback.Domain.DomainServices;

public interface IValidationServiceProxy
{
    Task<GeminiResponse> IsAcceptableContentAsync(string content);
    Task<GeminiResponse> IsAcceptableTitleAsync(string title);
}