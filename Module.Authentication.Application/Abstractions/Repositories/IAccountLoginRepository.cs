using Module.Authentication.Domain.Entity;

namespace Module.Authentication.Application.Abstractions.Repositories;

public interface IAccountLoginRepository
{
    Task<AccountLogin?> GetAccountLoginFromEmailAsync(string email);
    Task CreateAccountLoginAsync(AccountLogin accountLogin);
    Task<bool> DoesAccountLoginEmailExistAsync(string email);
}