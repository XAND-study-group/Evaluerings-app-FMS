using MediatR;
using Module.Authentication.Application.Abstractions.Repositories;
using Module.Authentication.Domain.DomainServices.Interfaces;
using Module.Authentication.Domain.Entity;
using Module.Shared.Domain.Models;

namespace Module.Authentication.Application.Features.Authentication.Commands;

public record AccountLoginCommand(string Email, string Password) : IRequest<Result<string>>;

public class AccountLoginCommandHandler : IRequestHandler<AccountLoginCommand, Result<string>> {
    private readonly IAccountLoginRepository _accountLoginRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public AccountLoginCommandHandler(IAccountLoginRepository accountLoginRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
    {
        _accountLoginRepository = accountLoginRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }
    
    public async Task<Result<string>> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
    {
        // TODO: Use DTO request instead
        var accountLogin = await _accountLoginRepository.GetAccountLoginFromEmailAsync(request.Email);

        if (accountLogin is null)
            return Result<string>.Create("Email eller adgangskode er forkert", "", ResultStatus.Error);

        var correctCredentials = _passwordHasher.Verify(request.Password, accountLogin.PasswordHash);

        // TODO: Change "new Account()" to the factory method
        return !correctCredentials ? 
            Result<string>.Create("Email eller adgangskode er forkert", "", ResultStatus.Error) : 
            Result<string>.Create("Success", _tokenProvider.Create(new Account()), ResultStatus.Success);
    }
}