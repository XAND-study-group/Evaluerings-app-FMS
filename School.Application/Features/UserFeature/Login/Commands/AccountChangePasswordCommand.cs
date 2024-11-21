using MediatR;
using Microsoft.Extensions.Caching.Memory;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountChangePasswordCommand(ChangePasswordRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;

public class AccountChangePasswordCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IMemoryCache memoryCache) : IRequestHandler<AccountChangePasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var changePasswordRequest = request.Request;

            var user =
                await userRepository.GetUserByIdAsync(changePasswordRequest.AccountLoginId);
            
            if (user is null)
                return Result<bool>.Create("Brugeren eksistere ikke", false, ResultStatus.Error);
            
            var code = memoryCache.Get(changePasswordRequest.AccountLoginId) as string;
            
            if (code == null || code != changePasswordRequest.Code)
                return Result<bool>.Create("Du kan ikke ændre adgangskoden lige nu", false, ResultStatus.Error);
            
            memoryCache.Remove(changePasswordRequest.AccountLoginId);
            
            user.ChangePassword(changePasswordRequest.NewPassword, passwordHasher);

            await userRepository.ChangeUserPasswordAsync();

            return Result<bool>.Create("Adgangskode er blevet ændret", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}