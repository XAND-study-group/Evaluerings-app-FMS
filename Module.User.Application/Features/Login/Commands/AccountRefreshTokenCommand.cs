﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Models;

namespace Module.User.Application.Features.Login.Commands;

public record AccountRefreshTokenCommand(TokenRequest Request) : IRequest<Result<TokenResponse?>>;

public class AccountRefreshTokenCommandHandler(
    ITokenProvider tokenProvider,
    IUserRepository userRepository,
    IConfiguration configuration)
    : IRequestHandler<AccountRefreshTokenCommand, Result<TokenResponse?>>
{
    public async Task<Result<TokenResponse?>> Handle(AccountRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshTokenRequest = request.Request;

        var storedUser = await userRepository.GetUserByRefreshTokenAsync(refreshTokenRequest.RefreshToken);

        if (storedUser is null || storedUser.RefreshToken.ExpirationDate < DateTime.Now)
            return Result<TokenResponse?>.Create("Din token er udløbet", null, ResultStatus.Error);

        var newAccessToken = tokenProvider.GenerateAccessToken(storedUser);
        var newRefreshToken = tokenProvider.GenerateRefreshToken();

        storedUser.SetRefreshToken(newRefreshToken, configuration.GetValue<int>("Jwt:RefreshTokenExpirationInDays"));
        await userRepository.SetUserRefreshTokenAsync(storedUser);
        
        return Result<TokenResponse?>.Create("Ny token blev genereret korrekt",
            new TokenResponse(newAccessToken, newRefreshToken), ResultStatus.Success);
    }
}