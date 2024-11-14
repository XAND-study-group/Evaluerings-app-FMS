using Module.User.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;

namespace Module.User.Application.Abstractions;

public interface IAccountLoginRepository
{
    Task<AccountLogin> GetAccountLoginFromIdAsync(Guid id);
    Task<AccountLogin?> GetAccountLoginFromEmailAsync(string email);
    Task CreateAccountLoginAsync(AccountLogin accountLogin);
    Task<bool> DoesAccountLoginEmailExistAsync(string email);
    Task ChangeLoginPasswordAsync();
    Task CreateAccountLoginsAsync(IEnumerable<AccountLogin> accountLogins);
}