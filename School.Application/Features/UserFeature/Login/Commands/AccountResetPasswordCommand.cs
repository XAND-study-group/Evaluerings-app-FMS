using MediatR;
using Microsoft.Extensions.Caching.Memory;
using School.Application.Abstractions.User;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountResetPasswordCommand(ResetPasswordRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;

public class AccountResetPasswordCommandHandler(
    IUserRepository userRepository,
    IMemoryCache memoryCache) : IRequestHandler<AccountResetPasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var changePasswordRequest = request.Request;

            var memoryValue = memoryCache.Get(changePasswordRequest.Code);

            if (memoryValue is not Guid userId)
                return Result<bool>.Create("Du kan ikke ændre din adgangskode", false, ResultStatus.Error);

            var user =
                await userRepository.GetUserByIdAsync(userId);

            if (user is null)
                return Result<bool>.Create("Brugeren eksistere ikke", false, ResultStatus.Error);

            memoryCache.Remove(changePasswordRequest.Code);

            user.ChangePassword(changePasswordRequest.NewPassword);

            await userRepository.ChangeUserPasswordAsync(user, user.RowVersion);

            return Result<bool>.Create("Adgangskode er blevet ændret", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}