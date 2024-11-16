using MediatR;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Models;

namespace Module.User.Application.Features.Login.Commands;

public record AccountLoginCommand(AuthenticateAccountLoginRequest Request) : IRequest<Result<TokenResponse?>>;

public class AccountLoginCommandHandler(
    IAccountLoginRepository accountLoginRepository,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : IRequestHandler<AccountLoginCommand, Result<TokenResponse?>>
{
    public async Task<Result<TokenResponse?>> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
    {
        // TODO: Change to try/catch
        var authenticateRequest = request.Request;

        var accountLogin = await accountLoginRepository.GetAccountLoginFromEmailAsync(authenticateRequest.Email);

        if (accountLogin is null)
            return Result<TokenResponse?>.Create("Email eller adgangskode er forkert", null, ResultStatus.Error);

        var correctCredentials = passwordHasher.Verify(authenticateRequest.Password, accountLogin.PasswordHash);

        var accessToken = tokenProvider.GenerateAccessToken(accountLogin.User);
        var refreshToken = tokenProvider.GenerateRefreshToken();

        var user = accountLogin.User;
        user.SetRefreshToken(refreshToken);

        await userRepository.SetUserRefreshTokenAsync(user);

        // TODO: Should there be "NEW here?
        return !correctCredentials
            ? Result<TokenResponse?>.Create("Email eller adgangskode er forkert", null, ResultStatus.Error)
            : Result<TokenResponse?>.Create("Success", new TokenResponse(accessToken, refreshToken),
                ResultStatus.Success);
    }
}