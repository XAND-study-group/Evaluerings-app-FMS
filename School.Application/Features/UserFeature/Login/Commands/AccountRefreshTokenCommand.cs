﻿using MediatR;
using Microsoft.Extensions.Configuration;
using School.Application.Abstractions.Semester;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Login.Commands;

public record AccountRefreshTokenCommand(TokenRequest Request)
    : IRequest<Result<TokenResponse?>>, ITransactionalCommand;

public class AccountRefreshTokenCommandHandler(
    ITokenProvider tokenProvider,
    IUserRepository userRepository,
    IClassRepository classRepository,
    IConfiguration configuration)
    : IRequestHandler<AccountRefreshTokenCommand, Result<TokenResponse?>>
{
    public async Task<Result<TokenResponse?>> Handle(AccountRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshTokenRequest = request.Request;

        var storedUser = await userRepository.GetUserByRefreshTokenAsync(refreshTokenRequest.RefreshToken);

        if (storedUser is null || storedUser.TryGetRefreshToken(refreshTokenRequest.RefreshToken).ExpirationDate <
            DateTime.Now)
            return Result<TokenResponse?>.Create("Din token er udløbet", null, ResultStatus.Error);

        storedUser.RemoveRefreshToken(refreshTokenRequest.RefreshToken);

        var userClasses = await classRepository.GetClassesByUserIdAsync(storedUser.Id);

        var newAccessToken = tokenProvider.GenerateAccessToken(storedUser, userClasses);
        var newRefreshToken = tokenProvider.GenerateRefreshToken();

        storedUser.AddRefreshToken(newRefreshToken, configuration.GetValue<int>("Jwt:RefreshTokenExpirationInDays"));
        await userRepository.AddUserRefreshTokenAsync(storedUser);

        return Result<TokenResponse?>.Create("Ny token blev genereret korrekt",
            new TokenResponse(newAccessToken, newRefreshToken), ResultStatus.Success);
    }
}