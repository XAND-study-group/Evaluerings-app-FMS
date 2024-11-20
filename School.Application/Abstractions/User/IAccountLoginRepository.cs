using School.Domain.Entities;

namespace School.Application.Abstractions.User;

public interface IAccountLoginRepository
{
    Task<AccountLogin?> GetAccountLoginFromIdAsync(Guid id);
    Task<AccountLogin?> GetAccountLoginFromEmailAsync(string email);
    Task CreateAccountLoginAsync(AccountLogin accountLogin);
    Task<bool> DoesAccountLoginEmailExistAsync(string email);
    Task ChangeLoginPasswordAsync(AccountLogin accountLogin);
    Task CreateAccountLoginsAsync(IEnumerable<AccountLogin> accountLogins);
}