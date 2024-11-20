using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.User.Application.Features.Login.Commands;

public record AccountRequestResetPasswordCommand(RequestResetPasswordRequest Request)
    : IRequest<Result<RequestResetPasswordResponse?>>;

public class AccountStartResetPasswordCommandHandler(
    IAccountLoginRepository loginRepository,
    ITokenProvider tokenProvider,
    IMemoryCache memoryCache)
    : IRequestHandler<AccountRequestResetPasswordCommand, Result<RequestResetPasswordResponse?>>
{
    public async Task<Result<RequestResetPasswordResponse?>> Handle(AccountRequestResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var startResetPasswordRequest = request.Request;

        var login = await loginRepository.GetAccountLoginFromIdAsync(startResetPasswordRequest.Id);

        if (login is null)
            return Result<RequestResetPasswordResponse?>.Create("Brugeren eksistere ikke", null, ResultStatus.Error);

        var code = tokenProvider.GenerateRandomCode();
        
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(15));

        memoryCache.Set(startResetPasswordRequest.Id, code, cacheEntryOptions);

        return Result<RequestResetPasswordResponse?>.Create("Der blev genereret en kode til at nulstille kodeordet",
            new RequestResetPasswordResponse(code), ResultStatus.Success);
    }
}