using MediatR;
using School.Application.Abstractions.User;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountSignOutCommand(SignOutRequest Request) : IRequest<Result<bool>>;

public class AccountSignOutCommandHandler(IUserRepository userRepository) : IRequestHandler<AccountSignOutCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountSignOutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var signOutRequest = request.Request;
        
            // Get
            var user = await userRepository.GetUserByIdAsync(signOutRequest.UserId);
        
            // Do
            if (user is null)
                return Result<bool>.Create("Brugeren eksistere ikke", false, ResultStatus.Error);
        
            user.RemoveRefreshToken(signOutRequest.RefreshToken);
        
            // Save
            await userRepository.SignOutUserAsync();
        
            return Result<bool>.Create("Brugeren blev logget ud", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create("Der skete en fejl", false, ResultStatus.Error);
        }
    }
}