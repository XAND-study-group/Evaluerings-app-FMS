namespace SharedKernel.Interfaces.DomainServices;

public interface IAiValidationService
{
    Task<bool> IsAcceptableContentAsync(string content);
    Task<bool> IsAcceptableTitleAsync(string title);
}