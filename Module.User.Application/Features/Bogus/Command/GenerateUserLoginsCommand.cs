using Bogus;
using MediatR;
using Module.Shared.Models;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using Module.User.Domain.Entities;

namespace Module.User.Application.Features.Bogus.Command;

public record GenerateUserLoginsCommand() : IRequest<Result<bool>>;

public class GenerateUserLoginsCommandHandler(IAccountLoginRepository accountLoginRepository, IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<GenerateUserLoginsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(GenerateUserLoginsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var accountLoginFake = new Faker<AccountLogin>()
                .CustomInstantiator(f => 
                    AccountLogin.Create(
                        f.Person.Email, 
                        "Password123.", 
                        Domain.Entities.User.Create(
                            f.Person.FirstName, 
                            f.Person.LastName, 
                            f.Person.Email), 
                        passwordHasher));
                // .RuleFor(account => account.Email, f => f.Person.Email)
                // .RuleFor(account => account.PasswordHash, f => passwordHasher.Hash("Password123."))
                // .RuleFor(account => account.User, f => Domain.Entities.User.Create(f.Person.FirstName, f.Person.LastName, f.Person.Email))

            var accounts = accountLoginFake.GenerateLazy(30);

            foreach (var accountLogin in accounts)
                await accountLoginRepository.CreateAccountLoginAsync(accountLogin);

            return Result<bool>.Create("Accounts have been created", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
        
    }
}