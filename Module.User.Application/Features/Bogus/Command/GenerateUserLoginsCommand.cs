using Bogus;
using MediatR;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using Module.User.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;
using SharedKernel.Models;

namespace Module.User.Application.Features.Bogus.Command;

public record GenerateUserLoginsCommand() : IRequest<Result<bool>>;

public class GenerateUserLoginsCommandHandler(
    IAccountLoginRepository accountLoginRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<GenerateUserLoginsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(GenerateUserLoginsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Role[] roles = [Role.User, Role.Teacher, Role.Admin];
            
            var accountLoginFake = new Faker<AccountLogin>()
                .CustomInstantiator(f =>
                    AccountLogin.Create(
                        f.Person.Email,
                        "Password123.",
                        Domain.Entities.User.Create(
                            f.Person.FirstName,
                            f.Person.LastName,
                            f.Person.Email),
                        f.PickRandom(roles),
                        passwordHasher)).UseSeed(420);

            var accounts = accountLoginFake.GenerateLazy(30);
            
            await accountLoginRepository.CreateAccountLoginsAsync(accounts);
            
            return Result<bool>.Create("Accounts have been created", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}