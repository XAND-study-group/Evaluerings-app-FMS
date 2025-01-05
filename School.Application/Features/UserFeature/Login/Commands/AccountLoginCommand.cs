using MediatR;
using Microsoft.Extensions.Configuration;
using School.Application.Abstractions.Semester;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountLoginCommand(AuthenticateAccountLoginRequest Request)
    : IRequest<Result<TokenResponse?>>, ITransactionalCommand;

public class AccountLoginCommandHandler(
    IUserRepository userRepository,
    ITokenProvider tokenProvider,
    IClassRepository classRepository,
    IConfiguration configuration) : IRequestHandler<AccountLoginCommand, Result<TokenResponse?>>
{
    public async Task<Result<TokenResponse?>> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var authenticateRequest = request.Request;

            var user = await userRepository.GetUserByEmailAsync(authenticateRequest.Email);

            if (user is null)
                return Result<TokenResponse?>.Create("Email eller adgangskode er forkert", null, ResultStatus.Error);

            var correctCredentials = user.PasswordHash.Verify(authenticateRequest.Password);

            if (!correctCredentials)
                return Result<TokenResponse?>.Create("Email eller adgangskode er forkert", null, ResultStatus.Error);

            var userClasses = await classRepository.GetClassesByUserIdAsync(user.Id);

            var accessToken = tokenProvider.GenerateAccessToken(user, userClasses);
            var refreshToken = tokenProvider.GenerateRefreshToken();

            user.AddRefreshToken(refreshToken, configuration.GetValue<int>("Jwt:RefreshTokenExpirationInDays"));

            await userRepository.AddUserRefreshTokenAsync(user);

            return Result<TokenResponse?>.Create("Success", new TokenResponse(accessToken, refreshToken),
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<TokenResponse?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}


