﻿using MediatR;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Models;

namespace Module.User.Application.Features.Login.Commands;

public record AccountChangePasswordCommand(ChangePasswordRequest Request) : IRequest<Result<bool>>;

public class AccountChangePasswordCommandHandler(
    IAccountLoginRepository accountLoginRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<AccountChangePasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var changePasswordRequest = request.Request;

            var accountLogin =
                await accountLoginRepository.GetAccountLoginFromIdAsync(changePasswordRequest.AccountLoginId);

            accountLogin.ChangePassword(changePasswordRequest.NewPassword, passwordHasher);

            await accountLoginRepository.ChangeLoginPasswordAsync();

            return Result<bool>.Create("Adgangskode er blevet ændret", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}