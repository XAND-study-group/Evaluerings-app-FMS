using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using School.Application.Abstractions.User;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountChangePasswordCommand(ChangePasswordRequest Request) : IRequest<Result<bool>>;

public class AccountChangePasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<AccountChangePasswordCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var changePasswordRequest = request.Request;
            // var httpContext = httpContextAccessor.HttpContext;
            //
            // if (httpContext is null)
            //     throw new Exception();
            //
            // var value = httpContext?.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;
            // var sid = Guid.Parse(value);
            // var role = httpContext?.User.FindFirst("Role")?.Value ?? string.Empty;
            //
            // if (sid != changePasswordRequest.UserId && role != "Admin")
            //     return Result<bool>.Create("Du prøver at ændre adgangskoden på en anden profil", false, ResultStatus.Error);
        
            // Get
            var user = await userRepository.GetUserByIdAsync(changePasswordRequest.UserId);
        
            // Do
            if (user is null)
                return Result<bool>.Create("Brugeren eksistere ikke", false, ResultStatus.Error);
        
            user.ChangePassword(changePasswordRequest.NewPassword);
        
            // Save
            await userRepository.ChangeUserPasswordAsync(user, changePasswordRequest.rowVersion);
            
            return Result<bool>.Create("Adgangskoden er blevet ændre", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create("Der skete en fejl", false, ResultStatus.Error);
        }
    }
}