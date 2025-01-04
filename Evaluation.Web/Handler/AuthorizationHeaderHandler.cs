using System.Net.Http.Headers;

namespace Evaluation.Web.Handler;

public class AuthorizationHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Get the JWT token from the provider
        var user = _httpContextAccessor.HttpContext.User;
        var token = user.FindFirst("JWT")?.Value;

        // Add the JWT token to the Authorization header
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmN2RjNDE5NC1kNzM5LTQwYjUtNjA3OS0wOGRkMTY0YTBjYmMiLCJlbWFpbCI6IlRpbW90aHkxNEB5YWhvby5jb20iLCJuYW1lIjoiVGltb3RoeSBIZWF0aGNvdGUiLCJSb2xlIjoiVXNlciIsIlBlcm1pc3Npb24iOlsiUmVhZEZlZWRiYWNrIiwiUG9zdEZlZWRiYWNrIiwiQ29tbWVudE9uRmVlZGJhY2siLCJBbnN3ZXJFeGl0U2xpcCIsIlZvdGVPbkZlZWRiYWNrIiwiUmVhZFJvb20iXSwiQ2xhc3MiOiJhZGYxZGUyYS0yNGFmLTRmMGEtZjdmZi0wOGRkMTY0YTBjYjAiLCJhdWQiOiJFdmFsdWF0aW5nQXBwLldlYiIsImlzcyI6IlNjaG9vbC5BUEkiLCJleHAiOjE4OTM2NzYzNDIsImlhdCI6MTczNTkwOTk0MiwibmJmIjoxNzM1OTA5OTQyfQ.KfYVMkFq8ppFYIr6ouzsN_y_jVbVJ0GKlx8KeUfmwN0");

        // Call the inner handler
        return await base.SendAsync(request, cancellationToken);
    }
}