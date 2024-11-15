namespace SharedKernel.Interfaces.DomainServices;

public interface IFeedbackAiService
{
    Task<bool> IsAcceptableContentAsync(string content);
    Task<bool> IsAcceptableTitleAsync(string title);
}