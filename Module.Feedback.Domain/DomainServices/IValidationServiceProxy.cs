namespace Module.Feedback.Domain.DomainServices;

public interface IValidationServiceProxy
{
    Task<bool> IsAcceptableContentAsync(string content);
    Task<bool> IsAcceptableTitleAsync(string title);
}