using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Services;
using SharedKernel.Dto.Features.School.User.Query;

namespace Module.Feedback.Infrastructure.Proxy;

public class SchoolApiProxy(IConfiguration configuration, HttpClient httpClient) : ISchoolApiProxy
{
    public async Task<IEnumerable<string>> GetEmailsByUserIdsAsync(IEnumerable<Guid> userIds)
    {
        var url = configuration["SchoolApiUrl:GetEmailsByUserIds"];

        var response = await httpClient.PostAsJsonAsync(url, new GetEmailsByUserIdsRequest(userIds));

        var responseContent = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        return responseContent ?? throw new Exception("Kunne ikke få fat i nogle emails");
    }
}