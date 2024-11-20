using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using SharedKernel.Models;
using SharedKernel.Dto.Features.School.Authentication.Command;

namespace Module.User.Application.Features.Login.Commands;

public record AccountChangePasswordCommand(ChangePasswordRequest Request) : IRequest<Result<bool>>;

public class AccountChangePasswordCommandHandler(
    IAccountLoginRepository accountLoginRepository,
    IPasswordHasher passwordHasher,
    IMemoryCache memoryCache) : IRequestHandler<AccountChangePasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var changePasswordRequest = request.Request;

            var accountLogin =
                await accountLoginRepository.GetAccountLoginFromIdAsync(changePasswordRequest.AccountLoginId);
            
            if (accountLogin is null)
                return Result<bool>.Create("Brugeren eksistere ikke", false, ResultStatus.Error);
            
            var code = memoryCache.Get(changePasswordRequest.AccountLoginId) as string;
            
            if (code == null && code != changePasswordRequest.Code)
                return Result<bool>.Create("Du kan ikke ændre adgangskoden lige nu", false, ResultStatus.Error);
            
            memoryCache.Remove(changePasswordRequest.AccountLoginId);
            
            accountLogin.ChangePassword(changePasswordRequest.NewPassword, passwordHasher);

            await accountLoginRepository.ChangeLoginPasswordAsync(accountLogin);

            return Result<bool>.Create("Adgangskode er blevet ændret", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}