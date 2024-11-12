﻿using Module.User.Domain.Entities;

namespace Module.User.Application.Abstractions;

public interface IAccountLoginRepository
{
    Task<AccountLogin?> GetAccountLoginFromEmailAsync(string email);
    Task CreateAccountLoginAsync(AccountLogin accountLogin);
    Task<bool> DoesAccountLoginEmailExistAsync(string email);
}