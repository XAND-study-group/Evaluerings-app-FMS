using MediatR;
using Microsoft.Extensions.Caching.Memory;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountRequestResetPasswordCommand(RequestResetPasswordRequest Request)
    : IRequest<Result<RequestResetPasswordResponse?>>;

public class AccountStartResetPasswordCommandHandler(
    IUserRepository userRepository,
    ITokenProvider tokenProvider,
    IMemoryCache memoryCache)
    : IRequestHandler<AccountRequestResetPasswordCommand, Result<RequestResetPasswordResponse?>>
{
    public async Task<Result<RequestResetPasswordResponse?>> Handle(AccountRequestResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var startResetPasswordRequest = request.Request;

        var login = await userRepository.GetUserByIdAsync(startResetPasswordRequest.Id);

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