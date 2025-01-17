﻿namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record CreateAccountLoginRequest(
    string Username,
    string Password,
    string Email,
    string Firstname,
    string Lastname);