using MediatR;
using School.Application.Abstractions.User;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountChangePasswordCommand(ChangePasswordRequest Request) : IRequest<Result<bool>>;

public class AccountChangePasswordCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AccountChangePasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var changePasswordRequest = request.Request;

            // Get
            var user = await userRepository.GetUserByIdAsync(changePasswordRequest.UserId);

            // Do
            if (user is null)
                return Result<bool>.Create("Brugeren eksistere ikke", false, ResultStatus.Error);

            user.ChangePassword(changePasswordRequest.NewPassword);

            // Save
            await userRepository.ChangeUserPasswordAsync(user, changePasswordRequest.RowVersion);

            return Result<bool>.Create("Adgangskoden er blevet ændre", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create("Der skete en fejl", false, ResultStatus.Error);
        }
    }
}