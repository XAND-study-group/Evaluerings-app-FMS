using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace Evaluation.Web.Services.Interfaces;

public interface ISchoolApiProxy
{
    Task<Result<TokenResponse?>> AuthenticateAsync(AuthenticateAccountLoginRequest request);
}