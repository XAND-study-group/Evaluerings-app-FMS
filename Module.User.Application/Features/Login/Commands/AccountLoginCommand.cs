using MediatR;
using Module.Shared.Models;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Authentication.Command;

namespace Module.User.Application.Features.Login.Commands;

public record AccountLoginCommand(AuthenticateAccountLoginRequest Request) : IRequest<Result<string>>;

public class AccountLoginCommandHandler(IAccountLoginRepository accountLoginRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider) : IRequestHandler<AccountLoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
    {
        var authenticateRequest = request.Request;
        
        var accountLogin = await accountLoginRepository.GetAccountLoginFromEmailAsync(authenticateRequest.Email);

        if (accountLogin is null)
            return Result<string>.Create("Email eller adgangskode er forkert", "", ResultStatus.Error);

        var correctCredentials = passwordHasher.Verify(authenticateRequest.Password, accountLogin.PasswordHash);
        
        return !correctCredentials ? 
            Result<string>.Create("Email eller adgangskode er forkert", "", ResultStatus.Error) : 
            Result<string>.Create("Success", tokenProvider.Create(accountLogin.User), ResultStatus.Success);
    }
}