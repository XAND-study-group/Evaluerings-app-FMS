using System.Net.Http.Headers;

namespace Evaluation.Web.Handler;

public class AuthorizationHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Get the JWT token from the provider
        var user = _httpContextAccessor.HttpContext.User;
        var token = user.FindFirst("JWT")?.Value;

        // Add the JWT token to the Authorization header
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Call the inner handler
        return await base.SendAsync(request, cancellationToken);
    }
}