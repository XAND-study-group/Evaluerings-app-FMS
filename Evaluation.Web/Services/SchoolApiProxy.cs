using Evaluation.Web.Services.Interfaces;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace Evaluation.Web.Services;

public class SchoolApiProxy(HttpClient httpClient) : ISchoolApiProxy
{
    async Task<Result<TokenResponse?>> ISchoolApiProxy.AuthenticateAsync(AuthenticateAccountLoginRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("Login", request);
        if (!response.IsSuccessStatusCode)
            throw new BadHttpRequestException(response.ReasonPhrase);

        return await response.Content.ReadFromJsonAsync<Result<TokenResponse?>>();
    }
}
